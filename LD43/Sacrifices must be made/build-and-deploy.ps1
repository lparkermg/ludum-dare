$version="0.0.0"
$buildTarget="Windows"
$deployTag=""
$projectName="Untitled Tilting"
$itchioProjectName="sacrifices-must-be-made"
$companyName="Luke Parker"

if($args[0]) {$version=$args[0]}

if($args[1]) {$buildTarget=$args[1]}

if($args[2]) {$deployTag=$args[2]}

Function Build([string]$target){
    Write-Host "Building $projectName ($target)"
    ./build.cmd "2018.2.2f1" $projectName $companyName $target $version
    
    if($LASTEXITCODE -ne 0){
        throw "Failed to build $projectName ($LASTEXITCODE)"
    }

    Write-Host "Build Complete ($target)"
}

Function Deploy([string]$target, [string]$channel){
    Write-Host "Deploying $projectName ($target) to itch.io"
    ./deploy.cmd $itchioProjectName $target $version $channel

    if($LASTEXITCODE -ne 0){
        throw "Failed to deploy $version to itch.io. ($LASTEXITCODE)"
    }

    Write-Host "Deployed $projectName ($target) version $version to itch.io"
}

Build $buildTarget

if($buildTarget -eq "BuildAll"){
    Deploy "MacOS" "osx$deployTag"
    Deploy "Linux" "linux$deployTag"
    Deploy "Windows" "windows$deployTag"
}
Else {
    Deploy $buildTarget $deployTag
}

