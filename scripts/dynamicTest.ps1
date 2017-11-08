$build = "& ../build.ps1"
$test = "& ../tests/DynamicTests/est.ps1"
$build
iex $build
iex $test