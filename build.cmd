@ECHO OFF

SETLOCAL

SET SCRIPTPATH=%~dp0
SET SCRIPTPATH=%SCRIPTPATH:~0,-1%

CD %SCRIPTPATH%

CALL ..\..\..\build\set35vars.bat

REM Build and sign the file
%msbuildexe% Cyotek.RegistryComparer.sln /p:Configuration=Release /verbosity:minimal /nologo /t:Clean,Build

IF NOT EXIST dist MKDIR dist

COPY /y .\src\Cyotek.RegistryComparer.Client\bin\release\Newtonsoft.Json.dll .\dist
COPY /y .\src\Cyotek.RegistryComparer.Client\bin\release\regcmpui.exe .\dist
COPY /y .\src\Cyotek.RegistryComparer.Client\bin\release\RegistryComparer.dll .\dist
COPY /y .\src\Cyotek.RegistryComparer.CommandLineInterface\bin\release\regcmp.exe .\dist

CALL signcmd .\dist\regcmp.exe
CALL signcmd .\dist\regcmpui.exe
CALL signcmd .\dist\RegistryComparer.dll

ENDLOCAL
