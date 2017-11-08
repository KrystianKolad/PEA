$currentLocation = $PSScriptRoot
$buildLocation = ".."
$testLocation = "./tests/DynamicTests/"
$build = "& ./build.ps1"
$test = "& ./test.ps1"
Set-Location $buildLocation
iex $build
Set-Location $testLocation
iex $test
Set-Location $currentLocation