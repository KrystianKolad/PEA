$currentLocation = $PSScriptRoot
$buildLocation = ".."
$testLocation = "./src/PEA/"
$build = "& ./build.ps1"
$run = "& ./run.ps1"
Set-Location $buildLocation
iex $build
Set-Location $testLocation
iex $run
Set-Location $currentLocation