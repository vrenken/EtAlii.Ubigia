Import-Module C:\TFS\EtAlii\EtAlii.Ubigia\Main\Sources\Client\Windows\Console\EtAlii.Ubigia.PowerShell.Test\bin\Debug\EtAlii.Ubigia.PowerShell.dll

Select-Storage http://localhost:64000 test 123

Add-Account peter 123

Get-Accounts

$entry = Add-Entry c:\test.jpg
$entry = Add-Entry c:\test-altered.jpg $entry

$entry = Get-Entry -Last
$entry = Get-Entry -First

$entries = Get-Entries -Root FileSystem
$entries = Get-Entries -Related $entry 5
$entries = Get-Entries -History $entry 5

$entries = Get-Entries -Related $entry
$entries = Get-Entries -History $entry


Register-RootEntry -Name $entry
Unregister-RootEntry -Name $entry 


$entry = Add-Entry -Root FileSystem -File c:\test.jpg
$entry = Add-Entry -Previous $previous -File c:\test.jpg
$entry = Add-Entry -Parent $parent -File c:\test.jpg
$entry = Add-Entry -Update $old -File c:\test.jpg

$entry = Add-Entry -Root FileSystem -Text "Test"
$entry = Add-Entry -Previous $previous -Text "Test"
$entry = Add-Entry -Parent $parent -Text "Test"
$entry = Add-Entry -Update $old -Text "Test"


$entry = Update-Entry $entry -Child $child
$entry = Update-Entry $entry -Next $next
$entry = Update-Entry $entry -Update $next

$entry = Update-Entry $entry -Text "Test"
$entry = Update-Entry $entry -File "c:\test.jpg"



$entry = Update-Entry c:\test.jpg

