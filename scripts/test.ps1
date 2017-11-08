$path = "cd ./tests/DynamicTests/ "
$build = "./build.ps1"
$test = $path + "./test.ps1"
iex $build
iex $test