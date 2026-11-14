param([string]$Python='python')
$ErrorActionPreference='Stop'
$vswhere=Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio/Installer/vswhere.exe'
$installation=& $vswhere -latest -prerelease -products '*' -requires Microsoft.VisualStudio.Component.VC.Tools.x86.x64 -property installationPath
if(-not $installation){throw 'Se requieren las herramientas C++ de Visual Studio.'}
$setup=Join-Path $installation 'Common7/Tools/VsDevCmd.bat'
$envLines=& cmd /d /s /c ('"'+$setup+'" -arch=x64 -host_arch=x64 >nul && set')
if($LASTEXITCODE -ne 0){throw 'No se pudo preparar el compilador.'}
foreach($line in $envLines){if($line -match '^([^=]+)=(.*)$'){[Environment]::SetEnvironmentVariable($matches[1],$matches[2],'Process')}}
& $Python (Join-Path $PSScriptRoot 'run-tests.py')
if($LASTEXITCODE -ne 0){throw 'La compilación o las pruebas fallaron.'}
