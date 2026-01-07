# PackAndPush.ps1
# Purpose: Pack from the desired configuration and push the newest .nupkg to NuGet.org
# Encoding: Save as UTF-8 with BOM

param(
    [ValidateSet("Debug","Release")]
    [string]$Configuration = "Debug"   # Optional; defaults to Debug
)

# Base paths
$repoRoot = $PSScriptRoot
$proj     = Join-Path $repoRoot "Common\Common.csproj"     # adjust if needed
$nugetSrc = "https://api.nuget.org/v3/index.json"
$pkgOut   = Join-Path $repoRoot "packages"

# Read BuildDir from the project file (optional property)
$xml = [xml](Get-Content $proj)
$buildDirNode = $xml.Project.PropertyGroup.BuildDir
$buildDir = if ($buildDirNode) { $buildDirNode[0] } else { $null }

# Fallback if BuildDir is not defined
if (-not $buildDir) {
    $buildDir = Join-Path $repoRoot ("Common\bin\" + $Configuration)
} elseif (-not [System.IO.Path]::IsPathRooted($buildDir)) {
    # Make BuildDir absolute if it was relative
    $buildDir = Join-Path $repoRoot $buildDir
}

# Print version number from the built assembly (no directory changes)
$asmPath = Join-Path $buildDir "WizardWrx.Common.dll"
if (Test-Path $asmPath) {
    $asm     = [System.Reflection.Assembly]::LoadFrom($asmPath)
    $verAttr = $asm.GetCustomAttributes([System.Reflection.AssemblyInformationalVersionAttribute], $false)[0]
    Write-Host "Packing and pushing version $($verAttr.InformationalVersion) from $Configuration"
} else {
    Write-Warning "Assembly not found at $asmPath. Version number cannot be read."
}

# Ensure output directory exists
if (-not (Test-Path $pkgOut)) { New-Item -ItemType Directory -Path $pkgOut | Out-Null }

# Pack from the repo root using absolute project path
nuget pack $proj `
    -Properties Configuration=$Configuration `
    -IncludeReferencedProjects `
    -OutputDir $pkgOut `
    -Verbosity normal

# Find the newest package
$pkg = Get-ChildItem (Join-Path $pkgOut "WizardWrx.Common.*.nupkg") |
       Sort-Object LastWriteTime -Descending |
       Select-Object -First 1

if (-not $pkg) {
    Write-Error "No package found in $pkgOut"
    exit 1
}

# Push to NuGet.org (API key read from %APPDATA%\NuGet\NuGet.Config)
nuget push $pkg.FullName -Source $nugetSrc

Write-Host "Successfully packed and pushed $($pkg.Name)"