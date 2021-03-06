param(
    [string]$version = "0.0.0",
    [string]$buildTarget = "Windows",
    [string]$deployTag = "",
    [boolean]$deploy = $False,
    [string]$deployChannel = "windows"
)

# PROJECT SPECIFIC STUFF

[string]$projectName = "A Project Name"
[string]$itchioProjectName = "itchio-project-name"
[string]$companyName = "Luke Parker"
[string]$itchUsername = "lparkermg"

# PATHS ETC DO NOT CHANGE
[string]$unityVersion="2019.3.10f1"
[string]$unityPath="E:\Programs\Unity\$unityVersion\Editor\Unity.exe"
[string]$buildLocation="$PSScriptRoot\Build\"

# Functions
Function Build([string]$target)
{
    Write-Host "Building $projectName ($target)"
    Write-Host "Version: $version"
    Write-Host ""
    Write-Host "Generating BuildManifest File"

    [string[]]$manifest = "ProductName=$projectName",
     "CompanyName=$companyName",
     "Version=$version",
     "BuildLocation=$buildLocation"

    Set-Content -Path "$PSScriptRoot\buildManifest.txt" -Value $manifest -Force
    Write-Host "Build Manifest Generated"

    Write-Host "Build Started"
    $unityParams = @("-quit", "-batchMode", "-projectPath",".","-logFile", "build.log", "-executeMethod", "BuildHelper.$Target")
    $buildProcess = Start-Process -File $unityPath -ArgumentList $unityParams -PassThru
    Wait-Process -InputObject $buildProcess
    Write-Host "Build Complete"

    Remove-Item -Path "$PSScriptRoot\buildManifest.txt" -Force
}

Function Deploy([string]$target){
    Write-Host "Deploying $projectName ($target)"
    Write-Host "Version: $version"
    Write-Host ""
    Write-Host "Deployment Starting"

    $butlerParams = @("push", "$buildLocation\Full", "$itchUsername/$itchioProjectName`:$deployChannel", "--userversion", $version )
    butler $butlerParams
    Write-Host "Deployment Complete"
}

Build($buildTarget)

if ($deploy -eq $True){
    Deploy($buildTarget)
}