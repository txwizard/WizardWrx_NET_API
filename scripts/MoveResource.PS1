param([String]$resourceNameSource, [String]$resourceNameDestination, [string] $keyName)
if(-not $resourceNameSource -or -not $resourceNameDestination -or -not $keyName) {
  echo "MoveResource.ps1 resourceNameSource resourceNameDestination keyName"
  return
}
 
$sourceFiles = Get-ChildItem "Resources\$resourceNameSource*.resx"
$destFiles = Get-ChildItem "Resources\$resourceNameDestination*.resx"
$mapping = @{}
 
$sourceFiles | foreach {
  $parts = $_ -split "\."
  $list = New-Object "system.collections.arraylist"
  $list.Add($_) > $null
  if($parts.Length -eq 2) {
    $mapping["en-us"] = $list
  }
  else {
    $mapping[$parts[1]] = $list
  }
}
 
$destFiles | foreach {
  $parts = $_ -split "\."
  if($parts.Length -eq 2) {
    $mapping["en-us"].Add($_ ) > $null
  }
  else {
    $mapping[$parts[1]].Add($_ ) > $null
  }
}
 
foreach($pair in $mapping.Values) {
  $sourcePath = $pair[0]
  $destPath = $pair[1]
  $source = [xml](Get-Content $sourcePath)
  $dest = [xml](Get-Content $destPath)
 
  $keyNodeSource = $source.root.data | Where-Object { $_.GetAttribute("name") -eq $keyName }
  $keyNodeDest = $dest.root.data | Where-Object { $_.GetAttribute("name") -eq $keyName }
  if ($keyNodeSource -eq $null) {
      echo "Key does not exist in source"
      return;
  }
  if($keyNodeDest -ne $null) {
    echo "Key already exists in destination"
    return;
  }
  try
  {
    $newNode = $dest.ImportNode($keyNodeSource.Clone(), $true)
    $source.root.RemoveChild($keyNodeSource) > $null
    $dest.root.AppendChild($newNode) > $null
    $dest.Save($destPath)
    $source.Save($sourcePath)
  }
  catch {
    Write-Host "Error text: " $_
    return
  }
}
 