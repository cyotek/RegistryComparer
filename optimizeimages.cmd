@ECHO OFF
FOR /F "tokens=*" %%G IN ('dir /s /b /aa *.png') DO optipng.exe -nc -nb -o7 -full %%G
