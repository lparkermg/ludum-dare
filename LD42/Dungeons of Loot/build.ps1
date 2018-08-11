$version="0.0.0"
$buildTarget="Windows"
$projectName="Dungeons of Loot"
$companyName="Luke Parker"

if($args[0]) {$version=$args[0]}

if($args[1]) {$buildTarget=$args[1]}

Function Build([string]$target){
    Write-Host "Building $projectName ($target)"
    ./build.cmd "2018.2.3f1" $projectName $companyName $target $version
    
    if($LASTEXITCODE -ne 0){
        throw "Failed to build $projectName ($LASTEXITCODE)"
    }

    Write-Host "Build Complete ($target)"
}

Build $buildTarget