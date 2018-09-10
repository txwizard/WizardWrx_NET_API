param([String]$keyOld, [String]$keyNew)
if(-not $keyOld -or -not $keyOld) {
  echo "RenameResource.ps1 keyOld keyNew"
  return
}
 
$files=get-childitem Resources *.resx
 
foreach($file in $files) {
  $xml = [xml](Get-Content $file.FullName)
  $dataNode = $xml.root.data | Where-Object { $_.GetAttribute("name") -eq $keyOld }
  if ($dataNode -ne $null) {
    $dataNode.SetAttribute("name", $keyNew)
    $xml.Save($file.FullName)
  }
}