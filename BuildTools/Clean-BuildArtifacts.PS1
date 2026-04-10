param(
    [switch]$Force,
    [switch]$WhatIf
)

Write-Host "=== Solution Cleanup Utility ==="
Write-Host "Working directory: $PWD"
Write-Host ""

# 1. Verify solution file exists.
$solution = Get-ChildItem -Filter *.sln -ErrorAction SilentlyContinue

if (-not $solution) {
    Write-Host "ERROR: No .sln file found in this directory."
    Write-Host "This script must be run from the solution root."
    exit 1
}

Write-Host "Solution file detected: $($solution.Name)"
Write-Host ""

# 2. Verify packages directory exists.
if (-not (Test-Path ".\packages")) {
    Write-Host "WARNING: No 'packages' directory found."
    Write-Host "This is unusual for a packages.config solution."
    Write-Host "Aborting to avoid accidental damage."
    exit 1
}

# 3. Remove the Temp directory if present.
if (Test-Path ".\Temp") {
    Write-Host "Temp directory detected. It will be removed entirely."

    if ($WhatIf) {
        Write-Host "[WhatIf] Would remove: .\Temp"
    }
    else {
        Remove-Item -Recurse -Force ".\Temp"
        Write-Host "Removed: .\Temp"
    }

    Write-Host ""
}

# 4. Find bin/obj directories, excluding node_modules.
$allCandidates = Get-ChildItem -Recurse -Directory -Include bin,obj -ErrorAction SilentlyContinue

$targets = @()
$skipped = @()

foreach ($dir in $allCandidates) {
    if ($dir.FullName -match '\\node_modules\\') {
        $skipped += $dir
    }
    else {
        $targets += $dir
    }
}

# 5. Report skipped directories.
if ($skipped.Count -gt 0) {
    Write-Host "Skipping directories inside node_modules:"
    foreach ($s in $skipped) {
        Write-Host "  [SKIP] $s"
    }
    Write-Host ""
}

# 6. Report directories that WILL be removed.
if ($targets.Count -eq 0) {
    Write-Host "No bin/obj directories found outside node_modules. Nothing to clean."
    exit 0
}

Write-Host "The following directories will be removed:"
foreach ($t in $targets) {
    Write-Host "  $t"
}
Write-Host ""

# 7. Confirm unless -Force is used.
if (-not $Force) {
    $answer = Read-Host "Proceed with deletion? (Y/N)"
    if ($answer -notin @("Y","y")) {
        Write-Host "Aborted by user."
        exit 0
    }
}

# 8. Perform deletion (or simulate with -WhatIf).
foreach ($dir in $targets) {
    if ($WhatIf) {
        Write-Host "[WhatIf] Would remove: $dir"
    }
    else {
        Remove-Item -Recurse -Force $dir
        Write-Host "Removed: $dir"
    }
}

Write-Host ""
Write-Host "Cleanup complete."