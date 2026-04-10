param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectFolder
)

Write-Host "=== SignalR Code Usage Scanner ==="
Write-Host "Scanning project: $ProjectFolder"
Write-Host ""

$patterns = @(
    "using Microsoft.AspNet.SignalR",
    "Hub\b",
    "HubContext",
    "IHubContext",
    "PersistentConnection",
    "GlobalHost",
    "MapSignalR",
    "HubName",
    "SignalR"
)

$hits = @()

foreach ($pattern in $patterns) {
    $results = Get-ChildItem -Recurse -Path $ProjectFolder -Include *.cs |
               Select-String -Pattern $pattern -SimpleMatch -ErrorAction SilentlyContinue

    if ($results) {
        $hits += $results
    }
}

if ($hits.Count -eq 0) {
    Write-Host "No SignalR usage found in code."
    Write-Host ""
    Write-Host "Scan complete."
    exit 0
}

Write-Host "SignalR usage found in:"
$hits | ForEach-Object { Write-Host "  $($_.Path)" }
Write-Host ""
Write-Host "Scan complete."