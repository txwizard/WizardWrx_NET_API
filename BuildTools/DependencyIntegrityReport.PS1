param(
    [Parameter(Mandatory=$true)]
    [string]$SolutionRoot
)

# Temporary file to hold the raw list of .csproj files
$projectListFile = Join-Path $env:TEMP "csproj_list.txt"

# Generate the list using dirx.exe (recursive, bare, suppress I/O errors)
# Example: dirx.exe D:\...\LeadLife\*.csproj /s /b /z
& dirx.exe "$SolutionRoot\*.csproj" /s /b /z > $projectListFile

# Load and sanitize the list:
# - remove blank lines
# - require .csproj suffix
# - require that the path actually exists
$projects = Get-Content $projectListFile |
    Where-Object {
        $_ -and
        $_.Trim().Length -gt 0 -and
        $_.ToLower().EndsWith(".csproj") -and
        (Test-Path $_)
    }

$rows = @()

foreach ($projPath in $projects) {

    # Normalize and load project file
    $proj = Get-Item $projPath
    $projName = $proj.BaseName
    $projDir  = $proj.DirectoryName

    [xml]$xml = Get-Content $proj.FullName

    #
    # 1. PACKAGES.CONFIG (legacy NuGet)
    #
    $pkgConfig = Join-Path $projDir "packages.config"
    if (Test-Path $pkgConfig) {
        [xml]$pc = Get-Content $pkgConfig
        foreach ($pkg in $pc.packages.package) {
            $rows += [pscustomobject]@{
                Project          = $projName
                PackageId        = $pkg.id
                Version          = $pkg.version
                HintPath         = $null
                BindingRedirect  = $null
                AssemblyIdentity = $null
                BinFileSize      = $null
                BinLastWrite     = $null
                BinAsmVersion    = $null
            }
        }
    }

    #
    # 2. PACKAGE REFERENCES (modern NuGet)
    #
    $pkgRefs = $xml.Project.ItemGroup.PackageReference
    foreach ($pkg in $pkgRefs) {
        $rows += [pscustomobject]@{
            Project          = $projName
            PackageId        = $pkg.Include
            Version          = $pkg.Version
            HintPath         = $null
            BindingRedirect  = $null
            AssemblyIdentity = $null
            BinFileSize      = $null
            BinLastWrite     = $null
            BinAsmVersion    = $null
        }
    }

    #
    # 3. DIRECT REFERENCES WITH HINTPATH
    #
    $refs = $xml.Project.ItemGroup.Reference
    foreach ($ref in $refs) {
        $hint = $ref.HintPath
        if ($hint) {
            $leaf  = Split-Path $hint -Leaf
            $pkgId = $leaf.Split('.')[0]

            $rows += [pscustomobject]@{
                Project          = $projName
                PackageId        = $pkgId
                Version          = $null
                HintPath         = $hint
                BindingRedirect  = $null
                AssemblyIdentity = $null
                BinFileSize      = $null
                BinLastWrite     = $null
                BinAsmVersion    = $null
            }
        }
    }

    #
    # 4. BINDING REDIRECTS
    #
    $configPath = Join-Path $projDir "app.config"
    if (-not (Test-Path $configPath)) {
        $configPath = Join-Path $projDir "web.config"
    }

    if (Test-Path $configPath) {
        [xml]$cfg = Get-Content $configPath

        $redirects = $cfg.configuration.runtime.assemblyBinding.dependentAssembly
        foreach ($redir in $redirects) {

            $ai   = $redir.assemblyIdentity
            $name = $ai.name
            $pkt  = $ai.publicKeyToken
            $cult = $ai.culture
            $pa   = $ai.processorArchitecture

            $assemblyIdentityString = "<assemblyIdentity name=""$name"" publicKeyToken=""$pkt"" culture=""$cult"""
            if ($pa) {
                $assemblyIdentityString += " processorArchitecture=""$pa"""
            }
            $assemblyIdentityString += " />"

            $rows += [pscustomobject]@{
                Project          = $projName
                PackageId        = $name
                Version          = $null
                HintPath         = $null
                BindingRedirect  = $redir.bindingRedirect.newVersion
                AssemblyIdentity = $assemblyIdentityString
                BinFileSize      = $null
                BinLastWrite     = $null
                BinAsmVersion    = $null
            }
        }
    }

    #
    # 5. BIN\DEBUG DLL METADATA
    #
    $binDebug = Join-Path $projDir "bin\Debug"
    if (Test-Path $binDebug) {
        $dlls = Get-ChildItem -Path $binDebug -Filter *.dll -ErrorAction SilentlyContinue

        foreach ($dll in $dlls) {
            try {
                $asm        = [System.Reflection.AssemblyName]::GetAssemblyName($dll.FullName)
                $asmVersion = $asm.Version.ToString()
            }
            catch {
                $asmVersion = $null
            }

            $rows += [pscustomobject]@{
                Project          = $projName
                PackageId        = $dll.BaseName
                Version          = $null
                HintPath         = $dll.FullName
                BindingRedirect  = $null
                AssemblyIdentity = $null
                BinFileSize      = $dll.Length
                BinLastWrite     = $dll.LastWriteTime
                BinAsmVersion    = $asmVersion
            }
        }
    }
}

#
# PHASE 2 — POST-PROCESSING: Add diagnostic columns
#

$groups = $rows | Group-Object PackageId

foreach ($g in $groups) {
    $pkgId = $g.Name
    $items = $g.Group

    $nugetVersions = $items.Version      | Where-Object { $_ }
    $asmVersions   = $items.BinAsmVersion | Where-Object { $_ }

    $nugetDisagree = ($nugetVersions | Select-Object -Unique).Count -gt 1
    $asmDisagree   = ($asmVersions   | Select-Object -Unique).Count -gt 1

    foreach ($item in $items) {

        $br  = $item.BindingRedirect
        $asm = $item.BinAsmVersion

        $brMatchesAsm    = ($br -and $asm -and ($br -eq $asm))
        $binMatchesNuget = ($item.Version -and $asm -and ($item.Version -eq $asm))

        $item | Add-Member -NotePropertyName BindingRedirectMatchesAssemblyVersion      -NotePropertyValue $brMatchesAsm
        $item | Add-Member -NotePropertyName PackageVersionDisagreeAcrossProjects       -NotePropertyValue $nugetDisagree
        $item | Add-Member -NotePropertyName AssemblyVersionDisagreeAcrossProjects      -NotePropertyValue $asmDisagree
        $item | Add-Member -NotePropertyName BinDllVersionMatchesNuGetVersion           -NotePropertyValue $binMatchesNuget
    }
}

#
# FINAL EXPORT
#

$csvPath = Join-Path $SolutionRoot "NuGetInventory.csv"

$rows = $rows | Where-Object {
    $_.PackageId        -or
    $_.Version          -or
    $_.HintPath         -or
    $_.BindingRedirect  -or
    $_.AssemblyIdentity -or
    $_.BinAsmVersion
}

$rows |
    Sort-Object PackageId, Project |
    Export-Csv -Path $csvPath -NoTypeInformation

Write-Host "Dependency Integrity Report generated:"
Write-Host $csvPath