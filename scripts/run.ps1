$currentLocation = $PSScriptRoot
$testLocation = "../src/PEA/"
$build = "& ./build.ps1"
$run = "& ./run.ps1"
iex $build
Set-Location $testLocation
iex $run
Set-Location $currentLocation