ECHO OFF

SETLOCAL

SET SCRIPTPATH=%~dp0
SET SCRIPTPATH=%SCRIPTPATH:~0,-1%

CD %SCRIPTPATH%\..\snapshots

SET ROOT=HKEY_CURRENT_USER\SOFTWARE\Cyotek\Registry Comparer\Tests
SET RC=..\src\Cyotek.RegistryComparer.CommandLineInterface\bin\debug\regcmp.exe
SET RCARGS=/key "%ROOT%" /nologo

CALL :createbase
%RC% %RCARGS% /name test_base

CALL :createbase
CALL :createsubkey3
%RC% %RCARGS% /name test_newsubkey

CALL :createbase
CALL :deletesubkey2
%RC% %RCARGS% /name test_delsubkey

CALL :createbase
CALL :deletevalue2
%RC% %RCARGS% /name test_delvalue

CALL :createbase
CALL :addvalue
%RC% %RCARGS% /name test_newvalue

CALL :createbase
CALL :changevalues
%RC% %RCARGS% /name test_modvalue

CALL :createbase
CALL :createsubkey3
CALL :deletesubkey2
CALL :deletevalue2
CALL :addvalue
CALL :changevalues
%RC% %RCARGS% /name test_all

ENDLOCAL

GOTO :eof

:createbase
REG DELETE "%ROOT%" /F
REG ADD    "%ROOT%"
CALL :createsubkey1
CALL :createsubkey2
GOTO :eof

:createsubkey1
SET SUBKEY=%ROOT%\Alpha
REG ADD    "%SUBKEY%" /v string           /t REG_SZ         /d alpha
REG ADD    "%SUBKEY%" /v multi_string     /t REG_MULTI_SZ   /d beta\0gamma
REG ADD    "%SUBKEY%" /v expanded_string  /t REG_EXPAND_SZ  /d %%systemroot%%\delta
GOTO :eof

:createsubkey2
SET SUBKEY=%ROOT%\Beta
REG ADD    "%SUBKEY%" /v dword            /t REG_DWORD      /d 2147483647
REG ADD    "%SUBKEY%" /v qword            /t REG_QWORD      /d 9223372036854775807
REG ADD    "%SUBKEY%" /v binary           /t REG_BINARY     /d 455053494c4f4e
GOTO :eof

:createsubkey3
SET SUBKEY=%ROOT%\Gamma
REG ADD    "%SUBKEY%" /ve                 /t REG_SZ         /d zeta
GOTO :eof

:deletesubkey2
SET SUBKEY=%ROOT%\Beta
REG DELETE "%SUBKEY%" /F
GOTO :eof

:deletevalue2
SET SUBKEY=%ROOT%\Alpha
REG DELETE "%SUBKEY%" /v multi_string /F
GOTO :eof

:addvalue
SET SUBKEY=%ROOT%\Alpha
REG ADD    "%SUBKEY%" /v iota             /t REG_SZ         /d kappa                /f
GOTO :eof

:changevalues
SET SUBKEY=%ROOT%\Alpha
REG ADD    "%SUBKEY%" /v string           /t REG_SZ         /d eta                  /f
REG ADD    "%SUBKEY%" /v expanded_string  /t REG_EXPAND_SZ  /d %%systemroot%%\theta /f
GOTO :eof
