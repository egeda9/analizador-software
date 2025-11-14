#!/bin/bash

echo "========================================"
echo " ANALIZADOR DE ESPECIFICACIONES DE SOFTWARE"
echo "========================================"
echo ""
echo "Verificando .NET SDK..."
dotnet --version
if [ $? -ne 0 ]; then
    echo "ERROR: .NET SDK no está instalado."
    echo "Descárgalo desde: https://dotnet.microsoft.com/download"
    exit 1
fi

echo ""
echo "Restaurando paquetes NuGet..."
dotnet restore
if [ $? -ne 0 ]; then
    echo "ERROR: No se pudieron restaurar los paquetes."
    exit 1
fi

echo ""
echo "========================================"
echo " INICIANDO APLICACIÓN..."
echo "========================================"
echo ""
echo "La aplicación estará disponible en:"
echo "  - HTTPS: https://localhost:5001"
echo "  - HTTP:  http://localhost:5000"
echo ""
echo "Presiona Ctrl+C para detener el servidor."
echo "========================================"
echo ""

dotnet run
