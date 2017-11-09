$currentLocation = $PSScriptRoot
$testLocation = "../tests/DynamicTests/"
$build = "& ./build.ps1"
$test = "& ./test.ps1"
iex $build
Set-Location $testLocation
iex $test
Set-Location $currentLocation