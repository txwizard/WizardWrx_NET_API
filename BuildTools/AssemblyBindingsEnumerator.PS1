param(
    [Parameter(Mandatory=$true)]
    [string]$WebConfigPath
)

# Load XML safely
[xml]$xml = Get-Content $WebConfigPath

# Navigate to runtime/assemblyBinding
$ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
$ns.AddNamespace("asm", "urn:schemas-microsoft-com:asm.v1")

$bindings = $xml.configuration.runtime.assemblyBinding.dependentAssembly

if (-not $bindings) {
    Write-Host "No dependentAssembly entries found."
    exit
}

function Pad([string]$s, [int]$w) {
    if ($null -eq $s) { $s = "<null>" }
    if ($s.Length -ge $w) { return $s }
    return $s + (" " * ($w - $s.Length))
}

function PrintLine([string]$label, [string]$value) {
    "{0} {1}" -f (Pad $label 28), $value
}

# Extract normalized objects
$records = foreach ($dep in $bindings) {

    $id = $dep.assemblyIdentity
    $redir = $dep.bindingRedirect

    [PSCustomObject]@{
        Name           = $id.name
        PublicKeyToken = $id.publicKeyToken
        OldVersion     = $redir.oldVersion
        NewVersion     = $redir.newVersion
    }
}

# Sort by assembly name
$sorted = $records | Sort-Object Name

# Output DirX-style
"-------------------------------------------------------------------------------"
PrintLine "WebConfig:", $WebConfigPath
"-------------------------------------------------------------------------------"

foreach ($r in $sorted) {
    PrintLine "Assembly:",        $r.Name
    PrintLine "PublicKeyToken:",  $r.PublicKeyToken
    PrintLine "OldVersion:",      $r.OldVersion
    PrintLine "NewVersion:",      $r.NewVersion
    ""
}

"-------------------------------------------------------------------------------"
