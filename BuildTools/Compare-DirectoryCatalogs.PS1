<#
.SYNOPSIS
    Side-by-side directory comparison with assembly metadata, PKT, UTC timestamps,
    file sizes, optional SHA digests, color coding, summary, CSV, and Markdown table.

.DESCRIPTION
    Compares all files in DirA against like-named files in DirB.
    Produces:
        - Full catalog (side-by-side)
        - Color-coded console output (table-like)
        - Optional Markdown table (aligned)
        - Summary of mismatches and missing files
        - Optional CSV export
        - Optional SHA-256 / SHA-384 / SHA-512 digest comparison

#>

param(
    [Parameter(Mandatory=$true)]
    [string] $DirA,

    [Parameter(Mandatory=$true)]
    [string] $DirB,

    [ValidateSet("SHA256","SHA384","SHA512")]
    [string] $CompareDigest,

    [switch] $HashAllFiles,

    [string] $CsvPath,

    [switch] $Markdown
)

# --- Startup Banner ---------------------------------------------------------

$scriptPath = $MyInvocation.MyCommand.Path
$scriptInfo = Get-Item -LiteralPath $scriptPath

$dtmStartUtc   = [DateTime]::UtcNow
$dtmStartLocal = $dtmStartUtc.ToLocalTime()

Write-Host ""
Write-Host "=== Script Startup ===" -ForegroundColor Cyan
Write-Host ("Script:             {0}" -f $scriptPath)
Write-Host ("Last Modified UTC:  {0:yyyy-MM-dd HH:mm:ss}" -f $scriptInfo.LastWriteTimeUtc)
Write-Host ("Start Time UTC:     {0:yyyy-MM-dd HH:mm:ss}" -f $dtmStartUtc)
Write-Host ("Start Time Local:   {0:yyyy-MM-dd HH:mm:ss}" -f $dtmStartLocal)
Write-Host ""

# --- Parameter Report -------------------------------------------------------

Write-Host "=== Parameters ===" -ForegroundColor Cyan
Write-Host ("DirA:               {0}" -f $DirA)
Write-Host ("DirB:               {0}" -f $DirB)

if ($CompareDigest) {
    Write-Host ("CompareDigest:      {0}" -f $CompareDigest)
}
else {
    Write-Host ("CompareDigest:      {0}" -f "(none)")
}

Write-Host ("HashAllFiles:       {0}" -f $HashAllFiles)
Write-Host ("Markdown:           {0}" -f $Markdown)

if ($CsvPath) {
    Write-Host ("CsvPath:            {0}" -f $CsvPath)
}
else {
    Write-Host ("CsvPath:            {0}" -f "(none)")
}

Write-Host ""

# --- Digest Function --------------------------------------------------------

function Get-Digest {
    param(
        [string] $Path,
        [string] $Algorithm
    )

    $hash = switch ($Algorithm.ToUpper()) {
        "SHA256" { [System.Security.Cryptography.SHA256]::Create() }
        "SHA384" { [System.Security.Cryptography.SHA384]::Create() }
        "SHA512" { [System.Security.Cryptography.SHA512]::Create() }
    }

    try {
        $stream = [System.IO.File]::OpenRead($Path)
        try {
            $digestBytes = $hash.ComputeHash($stream)
            ($digestBytes | ForEach-Object { $_.ToString("x2") }) -join ''
        }
        finally {
            $stream.Dispose()
        }
    }
    catch {
        ""
    }
    finally {
        $hash.Dispose()
    }
}

# --- File Catalog -----------------------------------------------------------

function Get-FileCatalog {
    param([string] $Path)

    Get-ChildItem -Path $Path -File |
        ForEach-Object {
            $item = $_
            $asm  = $null
            $tok  = ""

            if ($item.Extension -in '.dll', '.exe') {
                try {
                    $asm = [System.Reflection.AssemblyName]::GetAssemblyName($item.FullName)
                    $tok = ($asm.GetPublicKeyToken() | ForEach-Object { $_.ToString("x2") }) -join ''
                }
                catch {
                    $asm = $null
                    $tok = ""
                }
            }

            [PSCustomObject]@{
                FileName          = $item.Name
                FullPath          = $item.FullName
                SizeBytes         = $item.Length
                LastWriteTimeUtc  = $item.LastWriteTimeUtc
                AssemblyVersion   = if ($asm) { $asm.Version.ToString() } else { "" }
                FileVersion       = $item.VersionInfo.FileVersion
                ProductVersion    = $item.VersionInfo.ProductVersion
                PublicKeyToken    = $tok
            }
        }
}

Write-Host ""
Write-Host "=== Building catalogs ===" -ForegroundColor Cyan

$catA = Get-FileCatalog -Path $DirA
$catB = Get-FileCatalog -Path $DirB | Group-Object -Property FileName -AsHashTable

# --- Comparison -------------------------------------------------------------

$results = foreach ($rowA in $catA) {
    $rowB = $catB[$rowA.FileName]

    if ($rowB) {

        $digestA = ""
        $digestB = ""

        if ($CompareDigest) {
            $shouldHash = $HashAllFiles -or ($rowA.FullPath -match '\.(dll|exe)$')

            if ($shouldHash) {
                $digestA = Get-Digest -Path $rowA.FullPath -Algorithm $CompareDigest
                $digestB = Get-Digest -Path $rowB.FullPath -Algorithm $CompareDigest
            }
        }

        [PSCustomObject]@{
            FileName          = $rowA.FileName

            A_FileVersion     = $rowA.FileVersion
            B_FileVersion     = $rowB.FileVersion
            Version_Status    = if ($rowA.FileVersion -eq $rowB.FileVersion) { "MATCH" } else { "MISMATCH" }

            A_PublicKeyToken  = $rowA.PublicKeyToken
            B_PublicKeyToken  = $rowB.PublicKeyToken
            PKT_Status        = if ($rowA.PublicKeyToken -eq $rowB.PublicKeyToken) { "MATCH" } else { "MISMATCH" }

            A_LastWriteUtc    = $rowA.LastWriteTimeUtc
            B_LastWriteUtc    = $rowB.LastWriteTimeUtc
            Timestamp_Status  = if ($rowA.LastWriteTimeUtc -eq $rowB.LastWriteTimeUtc) { "MATCH" } else { "MISMATCH" }

            A_SizeBytes       = $rowA.SizeBytes
            B_SizeBytes       = $rowB.SizeBytes
            Size_Status       = if ($rowA.SizeBytes -eq $rowB.SizeBytes) { "MATCH" } else { "MISMATCH" }

            A_Digest          = $digestA
            B_Digest          = $digestB
            Digest_Status     = if ($CompareDigest -and ($digestA -ne $digestB)) { "MISMATCH" } elseif ($CompareDigest) { "MATCH" } else { "" }

            MissingInB        = $false
        }
    }
    else {
        [PSCustomObject]@{
            FileName          = $rowA.FileName

            A_FileVersion     = $rowA.FileVersion
            B_FileVersion     = ""
            Version_Status    = "ABSENT"

            A_PublicKeyToken  = $rowA.PublicKeyToken
            B_PublicKeyToken  = ""
            PKT_Status        = "ABSENT"

            A_LastWriteUtc    = $rowA.LastWriteTimeUtc
            B_LastWriteUtc    = ""
            Timestamp_Status  = "ABSENT"

            A_SizeBytes       = $rowA.SizeBytes
            B_SizeBytes       = ""
            Size_Status       = "ABSENT"

            A_Digest          = ""
            B_Digest          = ""
            Digest_Status     = "ABSENT"

            MissingInB        = $true
        }
    }
}

# --- Normalized Table Rows --------------------------------------------------

$tableRows = $results | ForEach-Object {
    [PSCustomObject]@{
        FileName          = $_.FileName
        VersionA          = $_.A_FileVersion
        VersionB          = $_.B_FileVersion
        PKT_A             = $_.A_PublicKeyToken
        PKT_B             = $_.B_PublicKeyToken
        TS_A              = $_.A_LastWriteUtc
        TS_B              = $_.B_LastWriteUtc
        SizeA             = $_.A_SizeBytes
        SizeB             = $_.B_SizeBytes
        DigestA           = $_.A_Digest
        DigestB           = $_.B_Digest
        Status_Version    = $_.Version_Status
        Status_PKT        = $_.PKT_Status
        Status_TS         = $_.Timestamp_Status
        Status_Size       = $_.Size_Status
        Status_Digest     = $_.Digest_Status
        MissingInB        = $_.MissingInB
    }
}

# --- Markdown Output --------------------------------------------------------

if ($Markdown) {

    Write-Host ""
    Write-Host "=== Markdown Table ===" -ForegroundColor Cyan

    $header = @(
        "FileName",
        "VersionA","VersionB",
        "PKT_A","PKT_B",
        "TS_A","TS_B",
        "SizeA","SizeB",
        "DigestA","DigestB",
        "Status_Version","Status_PKT","Status_TS","Status_Size","Status_Digest",
        "MissingInB"
    )

    # Compute column widths.
    $widths = @{}
    foreach ($col in $header) {
        $maxLen = ($tableRows | ForEach-Object { "$($_.$col)" }).ForEach({ $_.Length }) | Measure-Object -Maximum | Select-Object -ExpandProperty Maximum
        $widths[$col] = [Math]::Max($maxLen, $col.Length)
    }

    # Emit header.
    $headerLine = "| " + ($header | ForEach-Object { $_.Trim().PadRight($widths[$_]) }) -join " | " + " |"
    Write-Output $headerLine

    # Emit alignment row.
    $alignLine = "| " + ($header | ForEach-Object { ":".PadRight($widths[$_], "-") }) -join " | " + " |"
    Write-Output $alignLine

    # Emit rows.
    foreach ($row in $tableRows) {
        $values = $header | ForEach-Object { "$($row.$_)".PadRight($widths[$_]) }
        Write-Output ("| " + ($values -join " | ") + " |")
    }

    Write-Host ""
    Write-Host "Markdown table emitted." -ForegroundColor Green
}
else {

    Write-Host ""
    Write-Host "=== Side-by-side comparison (Table) ===" -ForegroundColor Cyan

    # Console table header
    Write-Host ("{0,-40}  {1,-20}  {2,-20}  {3,-25}  {4,-15}" -f `
        "FileName", "Ver(A/B)", "PKT(A/B)", "TS(A/B)", "Size(A/B)") -ForegroundColor White

    Write-Host ("{0}" -f ("-" * 130)) -ForegroundColor White

    foreach ($r in $results) {

        $msg = "{0,-40}  {1,-20}  {2,-20}  {3,-25}  {4,-15}" -f `
            $r.FileName.ToString(),
            ("{0}/{1}" -f $r.A_FileVersion, $r.B_FileVersion),
            ("{0}/{1}" -f $r.A_PublicKeyToken, $r.B_PublicKeyToken),
            ("{0}/{1}" -f $r.A_LastWriteUtc, $r.B_LastWriteUtc),
            ("{0}/{1}" -f $r.A_SizeBytes, $r.B_SizeBytes)

        $statuses = @(
            $r.Version_Status,
            $r.PKT_Status,
            $r.Timestamp_Status,
            $r.Size_Status,
            $r.Digest_Status
        )

        if ($r.MissingInB) {
            Write-Host $msg -ForegroundColor Yellow
        }
        elseif ($statuses -contains "MISMATCH") {
            Write-Host $msg -BackgroundColor DarkRed -ForegroundColor White
        }
        else {
            Write-Host $msg -BackgroundColor DarkGreen -ForegroundColor White
        }
    }
}

# --- Summary ---------------------------------------------------------------

Write-Host ""
Write-Host "=== Summary of differences ===" -ForegroundColor Cyan

$missing        = $results | Where-Object { $_.MissingInB }
$versionMismatch= $results | Where-Object { $_.Version_Status   -eq "MISMATCH" }
$pktMismatch    = $results | Where-Object { $_.PKT_Status       -eq "MISMATCH" }
$tsMismatch     = $results | Where-Object { $_.Timestamp_Status -eq "MISMATCH" }
$sizeMismatch   = $results | Where-Object { $_.Size_Status      -eq "MISMATCH" }
$digestMismatch = $results | Where-Object { $_.Digest_Status    -eq "MISMATCH" }

if ($missing.Count -gt 0) {
    Write-Host "`nMissing in B:" -ForegroundColor Yellow
    $missing | ForEach-Object { Write-Host ("  " + $_.FileName) }
}

if ($versionMismatch.Count -gt 0) {
    Write-Host "`nVersion mismatches:" -ForegroundColor Yellow
    foreach ($m in $versionMismatch) {
        Write-Host ("  {0}  A:{1}  B:{2}" -f $m.FileName, $m.A_FileVersion, $m.B_FileVersion)
    }
}

if ($pktMismatch.Count -gt 0) {
    Write-Host "`nPublicKeyToken mismatches:" -ForegroundColor Yellow
    foreach ($m in $pktMismatch) {
        Write-Host ("  {0}  A:{1}  B:{2}" -f $m.FileName, $m.A_PublicKeyToken, $m.B_PublicKeyToken)
    }
}

if ($tsMismatch.Count -gt 0) {
    Write-Host "`nTimestamp mismatches (UTC):" -ForegroundColor Yellow
    foreach ($m in $tsMismatch) {
        Write-Host ("  {0}  A:{1}  B:{2}" -f $m.FileName, $m.A_LastWriteUtc, $m.B_LastWriteUtc)
    }
}

if ($sizeMismatch.Count -gt 0) {
    Write-Host "`nSize mismatches:" -ForegroundColor Yellow
    foreach ($m in $sizeMismatch) {
        Write-Host ("  {0}  A:{1}  B:{2}" -f $m.FileName, $m.A_SizeBytes, $m.B_SizeBytes)
    }
}

if ($digestMismatch.Count -gt 0) {
    Write-Host "`nDigest mismatches ($CompareDigest):" -ForegroundColor Yellow
    foreach ($m in $digestMismatch) {
        Write-Host ("  {0}  A:{1}  B:{2}" -f $m.FileName, $m.A_Digest, $m.B_Digest)
    }
}

if ($missing.Count -eq 0 -and
    $versionMismatch.Count -eq 0 -and
    $pktMismatch.Count -eq 0 -and
    $tsMismatch.Count -eq 0 -and
    $sizeMismatch.Count -eq 0 -and
    $digestMismatch.Count -eq 0) {

    Write-Host "No differences detected." -ForegroundColor Green
}

# --- CSV Output ------------------------------------------------------------

if ($CsvPath) {
    Write-Host "`nWriting CSV to $CsvPath ..." -ForegroundColor Cyan
    $results | Export-Csv -Path $CsvPath -NoTypeInformation
    Write-Host "CSV written." -ForegroundColor Green
}

# --- Shutdown Banner --------------------------------------------------------

$dtmEndUtc   = [DateTime]::UtcNow
$dtmEndLocal = $dtmEndUtc.ToLocalTime()
$elapsed     = $dtmEndUtc - $dtmStartUtc

Write-Host ""
Write-Host "=== Script Completion ===" -ForegroundColor Cyan
Write-Host ("End Time UTC:       {0:yyyy-MM-dd HH:mm:ss}" -f $dtmEndUtc)
Write-Host ("End Time Local:     {0:yyyy-MM-dd HH:mm:ss}" -f $dtmEndLocal)
Write-Host ("Elapsed:            {0:hh\:mm\:ss}" -f $elapsed)
Write-Host ""

Write-Host "=== Done ===" -ForegroundColor Cyan