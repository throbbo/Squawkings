@ECHO OFF
src\.nuget\NuGet.exe install src\.nuget\packages.config -OutputDirectory src\packages
powershell -NoProfile -ExecutionPolicy Bypass -Command "& {Import-Module .\Tools\PSake\teamcity.psm1; Import-Module .\Tools\PSake\psake.psm1; Invoke-psake .\build.ps1; if ($psake.build_success -eq $false) { exit 1 } else { exit 0 } }"
IF (%1) NEQ (tc) PAUSE
