param (
	[string] $ServicePort,
	[string] $ServiceAssemblyName,
	[string] $LogFile
)

# Fail fast
$ErrorActionPreference = "Stop"

function Check-ExitCode() {
	$exitCode = $LASTEXITCODE
	if ($exitCode -ne 0) {
		exit $exitCode
	}
}

$user          = $env:USERNAME
#$discoveryPort = 1235
$url           = "http://+:$ServicePort/" # for netsh http [...] urlacl
$ruleBaseName  = "EtAlii Infrastructure Service"
#$clientApp     = "EtAlii.Ubigia.Windows.Touch"

# Match function for _our_ firewall rules
function Match-NetFirewallRule($rule) 
{
	# Rules from this (or a previous) script
	if ($rule.DisplayName.StartsWith($ruleBaseName)) 
	{
		return $true
	}

	# Generated rules by Windows Firewall prompt
	elseif ($ServiceAssemblyName) 
	{
		$ignoreCase = $true
		return [string]::Compare($ServiceAssemblyName, $rule.DisplayName, $ignoreCase) -eq 0
	}

	return $false
}

# Logging
if ($LogFile) 
{
	Start-Transcript -Path $LogFile -Force >$null
}

echo "Removing existing firewall rules"
echo "----------------------------------------------------------------------------------"
Get-NetFirewallRule | where { Match-NetFirewallRule $_ } | %{ Remove-NetFirewallRule -PassThru -Name $_.Name } # -PassThru outputs removed rules

echo "Adding firewall rules"
echo "----------------------------------------------------------------------------------"
New-NetFirewallRule -DisplayName "$ruleBaseName (Api)" -Direction Inbound -Action Allow -Protocol "TCP" -LocalPort $ServicePort
#New-NetFirewallRule -DisplayName "$ruleBaseName (Discovery)" -Direction Inbound -Action Allow -Protocol "UDP" -LocalPort $discoveryPort

echo "Adding HTTP URL ACL $url for user $user"
echo "----------------------------------------------------------------------------------"

# Suppress output since there will be an error if the ACL does not exist, which is okay
netsh http delete urlacl url=$url >$null

# Since Start-Transcript does not capture output of native commands, we do it manually
& netsh http add urlacl url=$url user=$user | Out-Default
Check-ExitCode

#echo "Adding loopback exemption for $clientApp"
#echo "----------------------------------------------------------------------------------"
#$appId = Get-AppxPackage -Name $clientApp | Select -First 1 -ExpandProperty PackageFamilyName
#if ($appId) {
#	# Will not create duplicates, so we keep it simple
#	& CheckNetIsolation.exe LoopbackExempt -a -n="$appId" | Out-Default
#	Check-ExitCode
#}
