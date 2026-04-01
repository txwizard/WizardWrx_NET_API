param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectFolder
)

Write-Host "=== OWIN Code Usage Scanner ==="
Write-Host "Scanning project: $ProjectFolder"
Write-Host ""

# 1. Look for OWIN Startup classes.
Write-Host "Checking for OWIN Startup classes..."
$startupFiles = Get-ChildItem -Recurse -Path $ProjectFolder -Include *.cs |
                Select-String -Pattern "OwinStartup|IAppBuilder|app.Use" -SimpleMatch

if ($startupFiles) {
    Write-Host "  Possible OWIN Startup files:"
    $startupFiles | ForEach-Object { Write-Host "    $($_.Path)" }
} else {
    Write-Host "  No OWIN Startup classes found."
}

Write-Host ""

# 2. Look for OWIN middleware usage.
Write-Host "Checking for OWIN middleware usage (app.Use...)"
$middleware = Get-ChildItem -Recurse -Path $ProjectFolder -Include *.cs |
              Select-String -Pattern "app\.Use" -SimpleMatch

if ($middleware) {
    Write-Host "  OWIN middleware usage found in:"
    $middleware | ForEach-Object { Write-Host "    $($_.Path)" }
} else {
    Write-Host "  No OWIN middleware usage found."
}

Write-Host ""

# 3. Look for OWIN authentication usage.
Write-Host "Checking for OWIN authentication usage..."
$auth = Get-ChildItem -Recurse -Path $ProjectFolder -Include *.cs |
        Select-String -Pattern "CookieAuthentication|OAuth|Bearer|UseAuth" -SimpleMatch

if ($auth) {
    Write-Host "  OWIN authentication usage found in:"
    $auth | ForEach-Object { Write-Host "    $($_.Path)" }
} else {
    Write-Host "  No OWIN authentication usage found."
}

Write-Host ""

Write-Host "Scan complete."