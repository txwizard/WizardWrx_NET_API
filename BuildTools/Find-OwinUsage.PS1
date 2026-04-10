Write-Host "=== OWIN Usage Scanner ==="
Write-Host ""

# OWIN package family prefix
$prefix = "Microsoft.Owin"

# Find all packages.config files.
$pkgFiles = Get-ChildItem -Recurse -Filter packages.config -ErrorAction SilentlyContinue

if (-not $pkgFiles) {
    Write-Host "No packages.config files found."
    exit 0
}

$matches = @()

foreach ($file in $pkgFiles) {
    $result = Select-String -Path $file.FullName -Pattern $prefix -SimpleMatch
    if ($result) {
        $matches += $file.FullName
    }
}

if ($matches.Count -eq 0) {
    Write-Host "No OWIN packages found in any project."
    exit 0
}

Write-Host "OWIN packages found in:"
foreach ($path in $matches | Sort-Object) {
    Write-Host "  $path"
}

Write-Host ""
Write-Host "Total projects referencing OWIN: $($matches.Count)"
Write-Host ""
Write-Host "Scan complete."
