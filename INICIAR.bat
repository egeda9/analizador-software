@echo off
echo ========================================
echo  ANALIZADOR DE ESPECIFICACIONES DE SOFTWARE
echo ========================================
echo.
echo Verificando .NET SDK...
dotnet --version
if %errorlevel% neq 0 (
    echo ERROR: .NET SDK no esta instalado.
    echo Descargalo desde: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)
echo.
echo Restaurando paquetes NuGet...
dotnet restore
if %errorlevel% neq 0 (
    echo ERROR: No se pudieron restaurar los paquetes.
    pause
    exit /b 1
)
echo.
echo ========================================
echo  INICIANDO APLICACION...
echo ========================================
echo.
echo La aplicacion estara disponible en:
echo   - HTTPS: https://localhost:5001
echo   - HTTP:  http://localhost:5000
echo.
echo Presiona Ctrl+C para detener el servidor.
echo ========================================
echo.
dotnet run
pause
