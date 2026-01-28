$ErrorActionPreference = 'Stop'

$solution = 'd:\OcApp\DownLoad\Standard Source Code 05-11-2025\Standard\VenusApplicationMerged - NewInterface\VenusApplication.sln'
$logPath = 'd:\OcApp\DownLoad\Standard Source Code 05-11-2025\Standard\VenusApplicationMerged - NewInterface\Scripts\msbuild-errors.log'

function Get-MsBuildPath {
  $vswhere1 = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
  $vswhere2 = Join-Path ${env:ProgramFiles} 'Microsoft Visual Studio\Installer\vswhere.exe'
  $vswhere = if (Test-Path $vswhere1) { $vswhere1 } elseif (Test-Path $vswhere2) { $vswhere2 } else { $null }
  if ($vswhere) {
    $path = & $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
    if ($path) { return $path }
  }
  $common = 'C:\Program Files\Microsoft Visual Studio','C:\Program Files (x86)\Microsoft Visual Studio'
  foreach ($root in $common) {
    if (Test-Path $root) {
      $candidates = Get-ChildItem -Path $root -Recurse -Filter MSBuild.exe -ErrorAction SilentlyContinue |
        Where-Object { $_.FullName -match '\\MSBuild\\.*\\Bin\\MSBuild.exe$' } |
        Select-Object -ExpandProperty FullName
      if ($candidates) { return ($candidates | Select-Object -First 1) }
    }
  }
  $cmd = Get-Command msbuild.exe -ErrorAction SilentlyContinue
  if ($cmd) { return $cmd.Source }
  return $null
}

$msbuild = Get-MsBuildPath
if ($msbuild) {
  & $msbuild $solution /t:Build /p:Configuration=Debug /m /nologo /v:m /clp:ErrorsOnly /fl /flp:"logfile=$logPath;errorsonly"
  exit $LASTEXITCODE
} else {
  $dotnet = Get-Command dotnet -ErrorAction SilentlyContinue
  if ($dotnet) {
    & $dotnet msbuild $solution -t:Build -p:Configuration=Debug -v:m -nologo -clp:ErrorsOnly -fl -flp:"logfile=$logPath;errorsonly"
    exit $LASTEXITCODE
  } else {
    Write-Error 'MSBuild not found and dotnet not available.'
    exit 1
  }
}
