param(
    [Parameter(Mandatory=$true)]
    [string]$PackageId
)

Write-Host "=== Reverse Dependency Scanner ==="
Write-Host "Finding packages that depend on: '$PackageId'"
Write-Host ""

# 1. Find all package roots.
$packageRoots = Get-ChildItem -Recurse -Directory -Filter packages -ErrorAction SilentlyContinue

if (-not $packageRoots) {
    Write-Host "ERROR: No 'packages' directories found anywhere under this solution."
    exit 1
}

Write-Host "Found $($packageRoots.Count) package root(s):"
foreach ($root in $packageRoots) {
    Write-Host "  $($root.FullName)"
}
Write-Host ""

# 2. Scan each root for versions of the target package.
Write-Host "Scanning for versions of '$PackageId' in each package root:"
foreach ($root in $packageRoots) {
    Write-Host "  In: $($root.FullName)"

    $matches = Get-ChildItem -Directory -Path $root.FullName -ErrorAction SilentlyContinue |
               Where-Object { $_.Name -like "$PackageId*" }

    if ($matches) {
        foreach ($m in $matches) {

            # Split folder name into parts.
            $parts = $m.Name.Split('.')

            # Identify numeric version segments at the end.
            $versionSegments = @()
            for ($i = $parts.Length - 1; $i -ge 0; $i--) {
                if ($parts[$i] -match '^\d+$') {
                    $versionSegments += $parts[$i]
                }
                else {
                    break
                }
            }

            # Reverse version segments (collected backwards).
            $versionSegments = $versionSegments[($versionSegments.Length - 1)..0]

            $version = ($versionSegments -join '.')
            $packageIdFull = ($parts[0..($parts.Length - $versionSegments.Length - 1)]) -join '.'

            Write-Host "    Package: $packageIdFull"
            Write-Host "    Version: $version"
            Write-Host "      Path: $($m.FullName)"
        }
    }
    else {
        Write-Host "    No versions found."
    }

    Write-Host ""   # Blank line between roots
}

# 3. Collect all nuspec files from all roots.
$nuspecs = foreach ($root in $packageRoots) {
    Get-ChildItem -Recurse -Filter *.nuspec -Path $root.FullName -ErrorAction SilentlyContinue
}

# 4. Scan nuspecs for dependencies.
$dependents = @()

foreach ($nuspec in $nuspecs) {
    try {
        $xml = [xml](Get-Content $nuspec.FullName)
        $deps = $xml.package.metadata.dependencies.dependency

        foreach ($dep in $deps) {
            if ($dep.id -eq $PackageId) {
                $dependents += $nuspec.FullName
            }
        }
    }
    catch {
        # Ignore malformed nuspecs.
    }
}

# 5. Report reverse dependencies.
if ($dependents.Count -eq 0) {
    Write-Host "No packages depend on '$PackageId'."
    exit 0
}

Write-Host "Packages that depend on '$PackageId':"
foreach ($nuspec in $dependents | Sort-Object) {
    $xml = [xml](Get-Content $nuspec)
    $id = $xml.package.metadata.id
    $ver = $xml.package.metadata.version

    Write-Host "  $id ($ver)"
    Write-Host "    $nuspec"
}
Write-Host ""
Write-Host "Total dependent packages: $($dependents.Count)"
Write-Host ""
Write-Host "Scan complete."