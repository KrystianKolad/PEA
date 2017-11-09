$currentLocation = $PSScriptRoot
$buildLocation = ".."
$restore = "dotnet restore"
$build = "dotnet build"
Set-Location $buildLocation
iex $restore
iex $build
Set-Location $currentLocation