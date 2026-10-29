<#
    List-GitCommitFiles.PS1 — Enumerate files included in a Git commit

    DEFAULT:
        Shows only A/M/D (Added / Modified / Deleted)

    SWITCHES:
        -IncludeRenames      → Adds R and B
        -IncludeCopies       → Adds C
        -IncludeTypeChanges  → Adds T
        -IncludeUnmerged     → Adds U
        -IncludeUnknown      → Adds X and ? (if present)
        -IncludeAll          → Enables all of the above

        -IncludeBody         → Include commit message body
        -NoMeta              → Suppress commit metadata

        -Output <csv>        → Write CSV to disk (console always shows metadata)
        -Help                → Show help
#>

param(
    [switch] $Help,
    [string] $Commit = "HEAD",
    [string] $Output,

    [switch] $IncludeRenames,
    [switch] $IncludeCopies,
    [switch] $IncludeTypeChanges,
    [switch] $IncludeUnmerged,
    [switch] $IncludeUnknown,
    [switch] $IncludeAll,

    [switch] $IncludeBody,
    [switch] $NoMeta
)

function Show-Help {
@"
List-GitCommitFiles.ps1 — Enumerate files included in a Git commit
------------------------------------------------------------------

USAGE:
    List-GitCommitFiles.ps1 [-Commit <hash>] [options]

DEFAULT:
    Shows only A/M/D (Added / Modified / Deleted)

OPTIONS:
    -Commit <hash>
        Commit to inspect. Defaults to HEAD.

    -IncludeRenames
        Add R and B (rename / broken pairing).

    -IncludeCopies
        Add C (copied).

    -IncludeTypeChanges
        Add T (type changed).

    -IncludeUnmerged
        Add U (unmerged paths).

    -IncludeUnknown
        Add X and ? (unknown / untracked).

    -IncludeAll
        Enable all of the above.

    -IncludeBody
        Include commit message body.

    -NoMeta
        Suppress commit metadata.

    -Output <csv>
        Write CSV to disk. Metadata is still printed to console.

    -Help
        Show this help text.
"@
}

if ($Help) { Show-Help; exit }

# Save original directory
$OriginalDir = Get-Location

# Find repo root
$Dir = $OriginalDir.Path
$RepoRoot = $null

while ($Dir -and (Test-Path $Dir)) {
    if (Test-Path (Join-Path $Dir ".git")) {
        $RepoRoot = $Dir
        break
    }
    $Dir = Split-Path $Dir -Parent
}

if (-not $RepoRoot) {
    Write-Error "Not inside a Git repository."
    exit 1
}

Set-Location $RepoRoot

try {
    # Resolve commit
    $FullCommit = git rev-parse $Commit 2>$null
    if (-not $FullCommit) {
        Write-Error "Commit '$Commit' could not be resolved."
        return
    }

    # Commit metadata
    $Meta = git show --no-patch --pretty=format:"%H%n%an%n%ae%n%ad%n%s" --date=iso $FullCommit
    $Lines = $Meta -split "`n"

    $CommitHash  = $Lines[0]
    $Author      = $Lines[1]
    $AuthorEmail = $Lines[2]
    $Date        = $Lines[3]
    $Subject     = $Lines[4]

    $Body = ""
    if ($IncludeBody) {
        $Body = git show --no-patch --pretty=format:"%b" $FullCommit
    }

    # Always write metadata to console
    if (-not $NoMeta) {
        Write-Host "Commit:      $CommitHash"
        Write-Host "Author:      $Author"
        Write-Host "Email:       $AuthorEmail"
        Write-Host "Date:        $Date"
        Write-Host "Subject:     $Subject"
        if ($IncludeBody -and $Body) {
            Write-Host "Body:"
            Write-Host $Body
        }
        Write-Host ""
    }

    # Full diff-tree output
    $Raw = git diff-tree --no-commit-id --name-status -r $FullCommit

    # Parse lines
    $Items = foreach ($Line in $Raw) {
        if (-not $Line.Trim()) { continue }

        $Parts = $Line -split "\s+"

        $Status = $Parts[0]

        if ($Status -match "^R" -or $Status -match "^C") {
            # Renames and copies have 3 fields
            $OldPath = $Parts[1]
            $NewPath = $Parts[2]
        } else {
            # Normal A/M/D/T/U/X
            $OldPath = $Parts[1]
            $NewPath = $Parts[1]
        }

        [PSCustomObject]@{
            Commit      = $CommitHash
            Status      = $Status
            OldPath     = $OldPath
            NewPath     = $NewPath
            Author      = $Author
            AuthorEmail = $AuthorEmail
            Date        = $Date
            Subject     = $Subject
            Body        = $Body
        }
    }

    # Default = A/M/D only
    $Allowed = @("A","M","D")

    if ($IncludeAll) {
        $Allowed = $null
    } else {
        if ($IncludeRenames)     { $Allowed += @("R","B") }
        if ($IncludeCopies)      { $Allowed += @("C") }
        if ($IncludeTypeChanges) { $Allowed += @("T") }
        if ($IncludeUnmerged)    { $Allowed += @("U") }
        if ($IncludeUnknown)     { $Allowed += @("X","?") }
    }

    if ($Allowed) {
        $Items = $Items | Where-Object { $Allowed -contains $_.Status }
    }

    # Console output
    foreach ($Item in $Items) {
        $Status = $Item.Status
        $Old    = $Item.OldPath
        $New    = $Item.NewPath

        if ($Old -ne $New) {
            Write-Host "$Status`t$Old → $New"
        } else {
            Write-Host "$Status`t$Old"
        }
    }

    # CSV output
    if ($Output) {
        $Items | Export-Csv -Path $Output -NoTypeInformation -Encoding UTF8
        Write-Host ""
        Write-Host "CSV written to $Output"
    }

}
finally {
    Set-Location $OriginalDir
}
