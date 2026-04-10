param(
    [Parameter(Mandatory=$true)]
    [string]$Package
)

Write-Host "=== Package Usage Scanner ==="
Write-Host "Searching for package: '$Package'"
Write-Host "Working directory: $PWD"
Write-Host ""

# 1. Verify solution file exists.
$solution = Get-ChildItem -Filter *.sln -ErrorAction SilentlyContinue
if (-not $solution) {
    Write-Host "ERROR: No .sln file found in this directory."
    Write-Host "Run this script from the solution root."
    exit 1
}

Write-Host "Solution file detected: $($solution.Name)"
Write-Host ""

# 2. Find all packages.config files.
$pkgFiles = Get-ChildItem -Recurse -Filter packages.config -ErrorAction SilentlyContinue

if (-not $pkgFiles) {
    Write-Host "No packages.config files found."
    exit 0
}

# 3. Search for the package name.
$matches = @()

foreach ($file in $pkgFiles) {
    $result = Select-String -Path $file.FullName -Pattern $Package -SimpleMatch
    if ($result) {
        $matches += $file.FullName
    }
}

# 4. Report results.
if ($matches.Count -eq 0) {
    Write-Host "Package '$Package' not found in any packages.config file."
    exit 0
}

Write-Host "Package '$Package' found in the following files:"
foreach ($path in $matches) {
    Write-Host "  $path"
}

Write-Host ""
Write-Host "Total matches: $($matches.Count)"
Write-Host ""
Write-Host "Scan complete."