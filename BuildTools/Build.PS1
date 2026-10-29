<#
    Build.ps1
    Structured MSBuild front-end for large C# solutions (LeadLife, Webhooks, etc.)

    - Finds the solution by walking upward from BuildTools (junction-safe)
    - Stores state (last project, last solution) in JSON next to the .sln
    - Groups projects by second namespace segment (LeadLife.Core.*, LeadLife.Data.*, etc.)
    - Presents collapsed groups with counts
    - Lets you:
        S = build startup project
        A = build entire solution
        L = build last selected project
        G = expand a group and pick a project
        F = filter projects by name
        X = exit
    - Uses MSBuild.exe with Normal verbosity
#>

Write-Host "=== Structured MSBuild Front-End ===" -ForegroundColor Cyan

# ---------- Helpers ----------


function Get-SolutionProjects {
    param([string]$solutionPath)

    $ext = [IO.Path]::GetExtension($solutionPath).ToLower()

    if ($ext -eq ".sln") {
        return Parse-SlnFile -path $solutionPath
    }
    elseif ($ext -eq ".slnx") {
        return Parse-SlnxFile -path $solutionPath
    }
    else {
        throw "Unsupported solution format: $solutionPath"
    }
}

function Parse-SlnFile {
    param([string]$path)

    $projects = @()

    foreach ($line in Get-Content -LiteralPath $path) {
        if ($line -match 'Project\(".*"\)\s*=\s*".*",\s*"([^"]+\.csproj)"') {
            $projects += (Resolve-Path -LiteralPath (Join-Path (Split-Path -Path $path -Parent) $matches[1]))
        }
    }

    return $projects
}

function Parse-SlnxFile {
    param([string]$path)

    [xml]$xml = Get-Content -LiteralPath $path

    $solutionDir = Split-Path -Path $path -Parent
    $projects = @()

    foreach ($proj in $xml.Solution.Project) {
        $full = Resolve-Path -LiteralPath (Join-Path $solutionDir $proj.Path)
        $projects += $full
    }

    return $projects
}

function Resolve-ProjectPaths {
    param(
        [string]$solutionPath,
        [string[]]$projectPaths
    )

    $solutionDir = Split-Path -Path $solutionPath -Parent

    $fullPaths = @()

    foreach ($proj in $projectPaths) {
        $fullPaths += (Resolve-Path -LiteralPath (Join-Path $solutionDir $proj))
    }

    return $fullPaths
}


function Get-RealScriptDir {
    $scriptDir = $PSScriptRoot
    try {
        $item = Get-Item -Path $scriptDir -ErrorAction Stop
        if ($item.Target) {
            return $item.Target
        }
    } catch {
        # Fall back if anything goes wrong
    }
    return $scriptDir
}

function Find-SolutionUpward {
    param(
        [string]$startDir
    )

    $dir = $startDir

    while ($true) {
        $slns = Get-ChildItem -Path $dir -Filter *.sln* -File -ErrorAction SilentlyContinue

        if ($slns.Count -eq 1) {
            return $slns[0].FullName
        } elseif ($slns.Count -gt 1) {
            Write-Host "Multiple solutions found in ${dir}:" -ForegroundColor Yellow
            $i = 1

            foreach ($s in $slns) {
                Write-Host "[$i] $($s.Name)" -ForegroundColor White
                $i++
            }

            $choice = Read-Host "Select solution by number"

            if ($choice -match '^\d+$' -and [int]$choice -ge 1 -and [int]$choice -le $slns.Count) {
                return $slns[[int]$choice - 1].FullName
            }
        }

        $parent = Split-Path -Path $dir -Parent

        if (-not $parent -or $parent -eq $dir) {
            break
        }

        $dir = $parent
    }

    throw "No solution (.sln) found walking upward from $startDir."
}

function Normalize-ProjectObjects {
    param([string[]]$paths)

    $projects = @()

    foreach ($p in $paths) {
        $leaf = Split-Path -Path $p -Leaf
        $name = [IO.Path]::GetFileNameWithoutExtension($leaf)

        $projects += [PSCustomObject]@{
            Name = $name
            Path = $p
        }
    }

    return $projects
}

function Get-StateFilePath {
    param(
        [string]$solutionPath
    )

    $solutionDir = Split-Path -Path $solutionPath -Parent
    return Join-Path $solutionDir "BuildState.json"
}

function Load-State {
    param(
        [string]$statePath
    )

    if (Test-Path $statePath) {
        try {
            return Get-Content $statePath -Raw | ConvertFrom-Json
        } catch {
            return [pscustomobject]@{}
        }
    }
    return [pscustomobject]@{}
}

function Save-State {
    param(
        [string]$statePath,
        [string]$solutionPath,
        [string]$lastProject
    )

    $obj = [pscustomobject]@{
        LastSolutionPath   = $solutionPath
        LastSelectedProject = $lastProject
    }

    $json = $obj | ConvertTo-Json -Depth 3
    Set-Content -Path $statePath -Value $json -Encoding UTF8
}

function Parse-ProjectsFromSolution {
    param([string]$solutionPath)

    $ext = [IO.Path]::GetExtension($solutionPath).ToLower()

    if ($ext -eq ".sln") {
        return Parse-SlnFile -path $solutionPath
    }
    elseif ($ext -eq ".slnx") {
        return Parse-SlnxFile -path $solutionPath
    }
    else {
        throw "Unsupported solution format: $solutionPath"
    }
}

function Get-StartupProjectFromSolution {
    param(
        [string]$solutionPath
    )

    $lines = Get-Content -Path $solutionPath
    $inExtGlobals = $false
    foreach ($line in $lines) {
        if ($line -match 'GlobalSection\(ExtensibilityGlobals\)') {
            $inExtGlobals = $true
            continue
        }
        if ($inExtGlobals -and $line -match 'EndGlobalSection') {
            $inExtGlobals = $false
            continue
        }
        if ($inExtGlobals -and $line -match 'StartupProject\s*=\s*(.+)') {
            $projName = $Matches[1].Trim()
            return $projName
        }
    }
    return $null
}

function Group-Projects {
    param(
        [array]$projects
    )

    $groups = @{}

    foreach ($p in $projects) {
        $name = $p.Name
        $segments = $name.Split('.')
        if ($segments.Count -ge 2) {
            $groupKey = $segments[1]  # second segment: Core, Data, Integration, Operations, Web, etc.
        } else {
            $groupKey = "Other"
        }

        if (-not $groups.ContainsKey($groupKey)) {
            $groups[$groupKey] = @()
        }
        $groups[$groupKey] += $p
    }

    return $groups
}

function Show-CollapsedGroups {
    param(
        [hashtable]$groups
    )

    Write-Host ""
    Write-Host "Project Groups:" -ForegroundColor Cyan
    foreach ($key in ($groups.Keys | Sort-Object)) {
        $count = $groups[$key].Count
        Write-Host ("  {0} ({1})" -f $key, $count) -ForegroundColor White
    }
    Write-Host ""
    Write-Host "[S] Startup project  [A] All projects  [L] Last project  [G] Pick from group  [F] Filter  [X] Exit" -ForegroundColor Yellow
}

function Pick-FromGroup {
    param(
        [hashtable]$groups
    )

    $groupName = Read-Host "Enter group name (e.g., Core, Data, Integration, Operations, Web, MVC, Other)"
    if (-not $groups.ContainsKey($groupName)) {
        Write-Host "Group '$groupName' not found." -ForegroundColor Red
        return $null
    }

    $projects = $groups[$groupName] | Sort-Object Name
    Write-Host ""
    Write-Host "Projects in group '$groupName':" -ForegroundColor Cyan
    $i = 1
    foreach ($p in $projects) {
        Write-Host ("[{0}] {1}" -f $i, $p.Name) -ForegroundColor White
        $i++
    }

    $choice = Read-Host "Select project by number"
    if ($choice -match '^\d+$') {
        $idx = [int]$choice
        if ($idx -ge 1 -and $idx -le $projects.Count) {
            return $projects[$idx - 1]
        }
    }

    Write-Host "Invalid selection." -ForegroundColor Red
    return $null
}

function Filter-Projects {
    param(
        [array]$projects
    )

    $term = Read-Host "Filter term (substring of project name)"
    if ([string]::IsNullOrWhiteSpace($term)) {
        Write-Host "Empty filter; returning." -ForegroundColor Yellow
        return $null
    }

    $matches = $projects | Where-Object { $_.Name -like "*$term*" } | Sort-Object Name
    if ($matches.Count -eq 0) {
        Write-Host "No projects match '$term'." -ForegroundColor Red
        return $null
    }

    Write-Host ""
    Write-Host "Filtered projects:" -ForegroundColor Cyan
    $i = 1
    foreach ($p in $matches) {
        Write-Host ("[{0}] {1}" -f $i, $p.Name) -ForegroundColor White
        $i++
    }

    $choice = Read-Host "Select project by number"
    if ($choice -match '^\d+$') {
        $idx = [int]$choice
        if ($idx -ge 1 -and $idx -le $matches.Count) {
            return $matches[$idx - 1]
        }
    }

    Write-Host "Invalid selection." -ForegroundColor Red
    return $null
}

function Invoke-MSBuildProject {
    param(
        [string]$solutionPath,
        [string]$projectPath
    )

    $msbuildCandidates = @(
        "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe",
        "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\amd64\MSBuild.exe"
    )

    $msbuild = $msbuildCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1

    if (-not $msbuild) {
        throw "MSBuild.exe not found in standard Visual Studio 18 locations."
    }

    Write-Host ""
    Write-Host "Building project:" -ForegroundColor Cyan
    Write-Host "  Project : $projectPath" -ForegroundColor White
    Write-Host ""

    & $msbuild $projectPath /v:normal
}

function Invoke-MSBuildSolution {
    param(
        [string]$solutionPath
    )

    $msbuild = "C:\Program Files\Microsoft Visual Studio\2026\Community\MSBuild\Current\Bin\MSBuild.exe"
    if (-not (Test-Path $msbuild)) {
        $msbuild = "MSBuild.exe"
    }

    Write-Host ""
    Write-Host "Building entire solution:" -ForegroundColor Cyan
    Write-Host "  Solution: $solutionPath" -ForegroundColor White
    Write-Host ""

    & $msbuild $solutionPath /t:Build /v:normal
}

function WaitForCarbonUnit {
    Write-Host ""
    Write-Host "Press <Enter> to exit..." -ForegroundColor Yellow
    [void][System.Console]::ReadLine()
}

# ---------- Main ----------

$realScriptDir = $PSScriptRoot

$solutionPath  = Find-SolutionUpward -startDir $realScriptDir
Write-Host "Solution: $solutionPath" -ForegroundColor Green

$statePath = Get-StateFilePath -solutionPath $solutionPath
$state     = Load-State -statePath $statePath

$rawProjects = Parse-ProjectsFromSolution -solutionPath $solutionPath
$projects    = Normalize-ProjectObjects -paths $rawProjects

if ($projects.Count -eq 0) {
    throw "No C# projects (.csproj) found in solution $solutionPath."
}

$groups    = Group-Projects -projects $projects
$startup   = Get-StartupProjectFromSolution -solutionPath $solutionPath

Write-Host ""
Write-Host ("Projects found: {0}" -f $projects.Count) -ForegroundColor Green
if ($startup) {
    Write-Host ("Startup project (from .sln): {0}" -f $startup) -ForegroundColor Green
} else {
    Write-Host "Startup project not defined in .sln (ExtensibilityGlobals)." -ForegroundColor Yellow
}

$exitRequested = $false

while (-not $exitRequested) {
    Show-CollapsedGroups -groups $groups
    $cmd = Read-Host "Select command (S/A/L/G/F/X)"

    switch -Regex ($cmd) {
        '^[Ss]$' {
            if (-not $startup) {
                Write-Host "No startup project defined in solution." -ForegroundColor Red
                continue
            }
            $proj = $projects | Where-Object { $_.Name -eq $startup }
            if (-not $proj) {
                Write-Host "Startup project '$startup' not found among parsed projects." -ForegroundColor Red
                continue
            }
            Invoke-MSBuildProject -solutionPath $solutionPath -projectPath $proj.Path
            Save-State -statePath $statePath -solutionPath $solutionPath -lastProject $proj.Name
        }
        '^[Aa]$' {
            Invoke-MSBuildSolution -solutionPath $solutionPath
            # Last project unchanged
        }
        '^[Ll]$' {
            if (-not $state.LastSelectedProject) {
                Write-Host "No last selected project stored." -ForegroundColor Yellow
                continue
            }
            $proj = $projects | Where-Object { $_.Name -eq $state.LastSelectedProject }
            if (-not $proj) {
                Write-Host "Last selected project '$($state.LastSelectedProject)' not found in this solution." -ForegroundColor Red
                continue
            }
            Invoke-MSBuildProject -solutionPath $solutionPath -projectPath $proj.Path
        }
        '^[Gg]$' {
            $proj = Pick-FromGroup -groups $groups
            if ($proj) {
                Invoke-MSBuildProject -solutionPath $solutionPath -projectPath $proj.Path
                Save-State -statePath $statePath -solutionPath $solutionPath -lastProject $proj.Name
            }
        }
        '^[Ff]$' {
            $proj = Filter-Projects -projects $projects
            if ($proj) {
                Invoke-MSBuildProject -solutionPath $solutionPath -projectPath $proj.Path
                Save-State -statePath $statePath -solutionPath $solutionPath -lastProject $proj.Name
            }
        }
        '^[Xx]$' {
            Write-Host "Exiting." -ForegroundColor Cyan
            return
        }
        default {
            Write-Host "Unknown command '$cmd'." -ForegroundColor Red
        }
    }
}

WaitForCarbonUnit