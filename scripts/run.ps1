$currentLocation = $PSScriptRoot
$testLocation = "../src/PEA/"
$build = "& ./build.ps1"
$run = "& dotnet run"
iex $build
Set-Location $testLocation
iex $run
Set-Location $currentLocation