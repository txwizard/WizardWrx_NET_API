param(
    [Parameter(Mandatory = $true)]
    [string]$RootPath
)

Write-Host "=== LESS Static Scanner ==="
Write-Host "Scanning directory: $RootPath"
Write-Host ""

# Use single-quoted strings so PowerShell does NOT interpret regex escapes.
$patterns = @{
    DynamicVariable    = '@\{[^\}]+\}'
    JsExpression       = '`[^`]+`'
    GuardedMixin       = 'when\s*\('
    MalformedMixin     = '\.[A-Za-z0-9_-]+\s*;'
    DivisionOperation  = '[0-9]+px\s*/\s*[0-9]+'
    EscapedSelector    = '~"[^"]+"'
    ImportReference    = '@import\s*\('
}

$lessFiles = Get-ChildItem -Path $RootPath -Recurse -Include *.less

if (-not $lessFiles) {
    Write-Host "No LESS files found."
    exit
}

$results = @()

foreach ($file in $lessFiles) {
    $content = Get-Content $file.FullName -Raw

    foreach ($key in $patterns.Keys) {
        $regex = $patterns[$key]
        $matches = [regex]::Matches($content, $regex)

        if ($matches.Count -gt 0) {
            foreach ($m in $matches) {
                $results += [pscustomobject]@{
                    File     = $file.FullName
                    Pattern  = $key
                    Match    = $m.Value
                }
            }
        }
    }
}

if ($results.Count -eq 0) {
    Write-Host "No suspicious patterns found. Static scan clean."
} else {
    Write-Host "Suspicious patterns detected:`n"
    $results | Format-List
}

Write-Host "`nScan complete."