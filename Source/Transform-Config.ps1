#!/usr/bin/env powershell
<#
.SYNOPSIS
    You can use this script to easly transform any XML file using XDT or JSON file using JDT.
    To use this script you can just save it locally and execute it. The script
    will download its dependencies automatically.
    
    # Source: 
    https://stackoverflow.com/questions/62944828/can-you-run-microsoft-visualstudio-slowcheetah-from-powershell
    
    # Example:
    Transform-Config "$scriptPath\Sample.config" "$scriptPath\Sample.Prod.config" "$scriptPath\Sample.Transformed.config"
#>
[cmdletbinding()]
param(
    [Parameter(
        Mandatory=$true,
        Position=0)]
    $sourceFile,

    [Parameter(
        Mandatory=$true,
        Position=1)]
    $transformFile,

    [Parameter(
        Mandatory=$true,
        Position=2)]
    $destFile
)

$loggingStubSource = @"
    using System;

    namespace Microsoft.VisualStudio.SlowCheetah
    {
        public class LoggingStub : ITransformationLogger
        {
            public void LogError(string message, params object[] messageArgs) { }
            public void LogError(string file, int lineNumber, int linePosition, string message, params object[] messageArgs) { }
            public void LogErrorFromException(Exception ex) { }
            public void LogErrorFromException(Exception ex, string file, int lineNumber, int linePosition) { }
            public void LogMessage(LogMessageImportance importance, string message, params object[] messageArgs) { }
            public void LogWarning(string message, params object[] messageArgs) { }
            public void LogWarning(string file, int lineNumber, int linePosition, string message, params object[] messageArgs) { }
        }
    }
"@    # this here-string terminator needs to be at column zero

<#
.SYNOPSIS
    If nuget is not in the tools
    folder then it will be downloaded there.
#>
function Get-Nuget(){
    [cmdletbinding()]
    param(
        $toolsDir = "$env:LOCALAPPDATA\NuGet\BuildTools\",
        $nugetDownloadUrl = 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'
    )
    process{
        $nugetDestPath = Join-Path -Path $toolsDir -ChildPath nuget.exe
        
        if(!(Test-Path $nugetDestPath)){
            'Downloading nuget.exe' | Write-Verbose
            # download nuget
            $webclient = New-Object System.Net.WebClient
            $webclient.DownloadFile($nugetDownloadUrl, $nugetDestPath)

            # double check that is was written to disk
            if(!(Test-Path $nugetDestPath)){
                throw 'unable to download nuget'
            }
        }

        # return the path of the file
        $nugetDestPath
    }
}

function Get-Nuget-Package(){
    [cmdletbinding()]
    param(
        [Parameter(
         Mandatory=$true,
         Position=0)]
        $packageName,
        [Parameter(
         Mandatory=$true,
         Position=1)]
        $toolFileName,
        $toolsDir = "$env:LOCALAPPDATA\NuGet\BuildTools\",
        $nugetDownloadUrl = 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe'
    )
    process{
        if(!(Test-Path $toolsDir)){ 
            New-Item -Path $toolsDir -ItemType Directory | Out-Null
        }

        $toolPath = (Get-ChildItem -Path $toolsDir -Include $toolFileName -Recurse) | Select-Object -First 1

        if($toolPath){
            return $toolPath
        }

        "Downloading package [$packageName] since it was not found in the tools folder [$toolsDir]" | Write-Verbose
        
        $cmdArgs = @('install',$packageName,'-OutputDirectory',(Resolve-Path $toolsDir).ToString())
        "Calling nuget.exe to download [$packageName] with the following args: [{0} {1}]" -f (Get-Nuget -toolsDir $toolsDir -nugetDownloadUrl $nugetDownloadUrl), ($cmdArgs -join ' ') | Write-Verbose
        &(Get-Nuget -toolsDir $toolsDir -nugetDownloadUrl $nugetDownloadUrl) $cmdArgs | Out-Null

        $toolPath = (Get-ChildItem -Path $toolsDir -Include $toolFileName -Recurse) | Select-Object -First 1
        return $toolPath
    }
}


function Transform-Config{
    [cmdletbinding()]
    param(
        [Parameter(
            Mandatory=$true,
            Position=0)]
        $sourceFile,

        [Parameter(
            Mandatory=$true,
            Position=1)]
        $transformFile,

        [Parameter(
            Mandatory=$true,
            Position=2)]
        $destFile,

        $toolsDir = "$env:LOCALAPPDATA\NuGet\BuildTools\"
    )
    process{
        $sourcePath    = (Resolve-Path $sourceFile).ToString()
        $transformPath = (Resolve-Path $transformFile).ToString()

        $cheetahPath = Get-Nuget-Package -packageName 'Microsoft.VisualStudio.SlowCheetah' -toolFileName 'Microsoft.VisualStudio.SlowCheetah.dll' -toolsDir $toolsDir

        if(!$cheetahPath){
            throw ('Failed to download Slow Cheetah package')
        }

        if (-not ([System.Management.Automation.PSTypeName]'Microsoft.VisualStudio.SlowCheetah.LoggingStub').Type)
        {
            [Reflection.Assembly]::LoadFrom($cheetahPath.FullName) | Out-Null       
            Add-Type -TypeDefinition $loggingStubSource -Language CSharp -ReferencedAssemblies $cheetahPath.FullName
        }
        $logStub = New-Object Microsoft.VisualStudio.SlowCheetah.LoggingStub

        $transformer = [Microsoft.VisualStudio.SlowCheetah.TransformerFactory]::GetTransformer($sourcePath, $logStub);
        $success = $transformer.Transform($sourcePath, $transformPath, $destFile);
        if(!$success){
            throw ("Transform of file [] failed!!!!")
        }
        Write-Host "Transform successful."
    }
}

Transform-Config -sourceFile $sourceFile -transformFile $transformFile -destFile $destFile