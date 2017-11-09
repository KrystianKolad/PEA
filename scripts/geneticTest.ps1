$currentLocation = $PSScriptRoot
$testLocation = "../tests/GeneticTests/"
$build = "& ./build.ps1"
$test = "& ./test.ps1"
iex $build
Set-Location $testLocation
iex $test
Set-Location $currentLocation