param([string]$ResultsDirectory = (Join-Path $PSScriptRoot 'results'))
$ErrorActionPreference = 'Stop'
$hotelRoot = Split-Path $PSScriptRoot -Parent
$instance = 'HotelQA_' + [Guid]::NewGuid().ToString('N').Substring(0, 12)
$created = $false
$oldConnection = $env:HOTELRESERVAS_CONNECTION_STRING
New-Item -ItemType Directory -Force -Path $ResultsDirectory | Out-Null
$ResultsDirectory = (Resolve-Path $ResultsDirectory).Path
Add-Type -AssemblyName System.Data
try {
    & SqlLocalDB create $instance -s
    if ($LASTEXITCODE -ne 0) { throw 'No se pudo crear LocalDB.' }
    $created = $true
    $env:HOTELRESERVAS_CONNECTION_STRING = "Server=(localdb)\$instance;Database=HotelDB;Integrated Security=True;Connect Timeout=30;"
    $masterConnection = "Server=(localdb)\$instance;Database=master;Integrated Security=True;Connect Timeout=30;"
    $connection = New-Object System.Data.SqlClient.SqlConnection $masterConnection
    try {
        $connection.Open()
        $versionCommand = $connection.CreateCommand()
        $versionCommand.CommandText = "SELECT CONVERT(varchar(30), SERVERPROPERTY('ProductVersion'))"
        $version = $versionCommand.ExecuteScalar()
        "SQL Server LocalDB $version" | Set-Content (Join-Path $ResultsDirectory 'installation.txt') -Encoding UTF8
        foreach ($file in @('HotelDB_Proyecto_Final.sql', 'HotelDB_Fase1_Migracion.sql', 'HotelDB_SeedData.sql')) {
            $sql = [IO.File]::ReadAllText((Join-Path $hotelRoot "Base de Datos/$file"))
            if ($file -eq 'HotelDB_Proyecto_Final.sql') {
                # LocalDB comparte una ruta predeterminada entre instancias del mismo usuario.
                # Los archivos de esta prueba van a su propia carpeta, sin reutilizar HotelDB.mdf.
                $dataFile = (Join-Path $ResultsDirectory ($instance + '.mdf')).Replace("'", "''")
                $logFile = (Join-Path $ResultsDirectory ($instance + '.ldf')).Replace("'", "''")
                $sql = $sql.Replace('CREATE DATABASE HotelDB;', "CREATE DATABASE HotelDB ON PRIMARY (NAME=N'HotelDB', FILENAME=N'$dataFile') LOG ON (NAME=N'HotelDB_log', FILENAME=N'$logFile');")
            }
            foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*(?:--[^\r\n]*)?\r?$')) {
                if ([string]::IsNullOrWhiteSpace($batch)) { continue }
                $command = $connection.CreateCommand()
                $command.CommandText = $batch
                $command.CommandTimeout = 60
                try { $null = $command.ExecuteNonQuery() }
                catch { throw "${file}: $($_.Exception.Message)" }
                finally { $command.Dispose() }
            }
            "PASS $file" | Add-Content (Join-Path $ResultsDirectory 'installation.txt') -Encoding UTF8
        }
    } finally { $connection.Dispose() }
    $app = Join-Path $hotelRoot 'HotelReservas/bin/Release/HotelReservas.exe'
    $testExe = Join-Path (Split-Path $app) 'HotelReservas.FunctionalTests.exe'
    $compiler = Join-Path $env:WINDIR 'Microsoft.NET/Framework64/v4.0.30319/csc.exe'
    & $compiler /nologo /target:exe /platform:anycpu "/out:$testExe" "/reference:$app" /reference:System.Data.dll /reference:System.Windows.Forms.dll /reference:System.Drawing.dll (Join-Path $PSScriptRoot 'FunctionalTests.cs')
    if ($LASTEXITCODE -ne 0) { throw 'No se pudieron compilar las pruebas.' }
    & $testExe $ResultsDirectory
    if ($LASTEXITCODE -ne 0) { throw 'Hay pruebas funcionales fallidas. Consultar functional-results.tsv.' }
} finally {
    if ($created) {
        [System.Data.SqlClient.SqlConnection]::ClearAllPools()
        # Solo se limpia la instancia con nombre aleatorio creada por esta ejecución.
        $cleanup = New-Object System.Data.SqlClient.SqlConnection $masterConnection
        try {
            $cleanup.Open()
            $cmd = $cleanup.CreateCommand()
            $cmd.CommandText = "IF DB_ID('HotelDB') IS NOT NULL BEGIN ALTER DATABASE HotelDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE HotelDB; END"
            $null = $cmd.ExecuteNonQuery()
        } finally {
            $cleanup.Dispose()
            & SqlLocalDB stop $instance
            & SqlLocalDB delete $instance
        }
    }
    $env:HOTELRESERVAS_CONNECTION_STRING = $oldConnection
}
