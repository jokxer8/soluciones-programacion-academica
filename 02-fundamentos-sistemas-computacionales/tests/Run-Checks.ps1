param(
    [Parameter(Mandatory)][string]$NasmPath,
    [Parameter(Mandatory)][string]$FasmPath,
    [string]$OutputDirectory = (Join-Path $env:TEMP ('fsc-checks-' + [guid]::NewGuid().ToString('N')))
)
$ErrorActionPreference = 'Stop'
$root = Split-Path $PSScriptRoot -Parent
New-Item -ItemType Directory -Force $OutputDirectory | Out-Null
$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
$vs = & $vswhere -latest -prerelease -products '*' -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
if (!$vs) { throw 'Instale las herramientas de C++ de Visual Studio.' }
$vc = Join-Path $vs 'VC/Auxiliary/Build/vcvars64.bat'
$environment = & cmd /c ('"' + $vc + '" >nul && set')
foreach ($line in $environment) {
    if ($line -match '^([^=]+)=(.*)$' -and $Matches[1] -notin 'HOME','CODEX_HOME') {
        [Environment]::SetEnvironmentVariable($Matches[1], $Matches[2], 'Process')
    }
}
$results = [Collections.Generic.List[object]]::new()
function Record($name, $kind) { $results.Add([pscustomobject]@{name=$name; kind=$kind; status='PASS'}) }
function Compile-C($source, $name, $objects=@()) {
    $exe = Join-Path $OutputDirectory ($name + '.exe')
    $obj = Join-Path $OutputDirectory ($name + '.obj')
    $log = & cl /nologo /std:c17 /W4 /D_CRT_SECURE_NO_WARNINGS $source @objects "/Fe:$exe" "/Fo:$obj" 2>&1
    $log | Set-Content (Join-Path $OutputDirectory ($name + '.log'))
    if ($LASTEXITCODE -ne 0) { throw "Compilacion fallida: $source`n$log" }
    Record $name 'compilacion C'
    return $exe
}
function Run-Case($exe, $inputText, $pattern, $name, $cwd=$root) {
    $info = [Diagnostics.ProcessStartInfo]::new($exe)
    $info.UseShellExecute=$false
    $info.RedirectStandardInput=$true
    $info.RedirectStandardOutput=$true
    $info.RedirectStandardError=$true
    $info.WorkingDirectory=$cwd
    $process=[Diagnostics.Process]::Start($info)
    $stdout=$process.StandardOutput.ReadToEndAsync()
    $stderr=$process.StandardError.ReadToEndAsync()
    $process.StandardInput.Write($inputText)
    $process.StandardInput.Close()
    if (!$process.WaitForExit(5000)) { $process.Kill($true); throw "Tiempo agotado: $name" }
    $output=$stdout.GetAwaiter().GetResult()
    $errorText=$stderr.GetAwaiter().GetResult()
    $output | Set-Content (Join-Path $OutputDirectory ($name + '.txt'))
    if ($process.ExitCode -ne 0 -or $output -notmatch $pattern) { throw "Prueba fallida: $name`n$output`n$errorText" }
    Record $name 'ejecucion funcional'
}
$executables=@{}
Copy-Item -LiteralPath (Join-Path $root 'proyecto-final-paises/countries_of_the_world.csv') -Destination $OutputDirectory
foreach ($source in Get-ChildItem $root -Recurse -Filter '*.c' | Where-Object { $_.Directory.Name -notin 'tests','proyecto-hibrido-c-nasm' }) {
    $key=[IO.Path]::GetRelativePath($root,$source.FullName).Replace('\','/')
    $name=($key -replace '[^a-zA-Z0-9-]','_')
    $executables[$key]=Compile-C $source.FullName $name
}
$fixtures=Get-Content (Join-Path $PSScriptRoot 'cases.json') -Raw | ConvertFrom-Json
foreach($case in $fixtures) { Run-Case $executables[$case.source] $case.input $case.pattern $case.name $OutputDirectory }
$objects=@()
foreach($file in Get-ChildItem (Join-Path $root 'proyecto-hibrido-c-nasm') -Filter '*.asm') {
    $obj=Join-Path $OutputDirectory ($file.BaseName+'.obj')
    $staged=Join-Path $OutputDirectory $file.Name
    [IO.File]::WriteAllBytes($staged,[IO.File]::ReadAllBytes($file.FullName))
    & $NasmPath -f win64 $staged -o $obj
    if($LASTEXITCODE -ne 0) { throw "NASM: $file" }
    $objects+=$obj
    Record $file.Name 'ensamblado NASM'
}
$hybrid=Compile-C (Join-Path $root 'proyecto-hibrido-c-nasm/main.c') 'hibrido' $objects
Run-Case $hybrid "5`n30`n50`n10`n40`n20`n4`n-7`n`n" '(?s)numeros\[0\] = 10.*numeros\[4\] = 50.*El total es: 150.*Despues del intercambio:\s+x = -7, y = 4' 'hibrido-orden-suma-swap'
Run-Case $hybrid "0`nabc`n1`n8`n2`n3`n`n" '(?s)Tamano invalido.*debe ingresar un numero entero.*El total es: 8.*Despues del intercambio:\s+x = 3, y = 2' 'hibrido-validacion-entrada'
foreach($file in Get-ChildItem (Join-Path $root 'c-y-ensamblador/fasm-linux') -Filter '*.asm') {
    $staged=Join-Path $OutputDirectory $file.Name
    [IO.File]::WriteAllBytes($staged,[IO.File]::ReadAllBytes($file.FullName))
    $log=& $FasmPath $staged (Join-Path $OutputDirectory ($file.BaseName+'.elf')) 2>&1
    if($LASTEXITCODE -ne 0) { throw "FASM: $file`n$log" }
    Record $file.Name 'ensamblado FASM; sin ejecucion Linux'
}
$test=Compile-C (Join-Path $PSScriptRoot 'countries-check.c') 'countries-check'
Run-Case $test '' 'COUNTRIES CHECKS PASS' 'paises-invariantes-csv' $OutputDirectory
$results | ConvertTo-Json -Depth 5 | Set-Content (Join-Path $OutputDirectory 'results.json')
$results | Group-Object kind | Select-Object Name,Count | Format-Table
Write-Output "Resultados: $OutputDirectory"
