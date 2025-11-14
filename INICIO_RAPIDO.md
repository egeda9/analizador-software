# 🚀 Guía Rápida de Inicio

## Paso 1: Obtener tu API Key de Anthropic

1. Ve a: https://console.anthropic.com/
2. Crea una cuenta o inicia sesión
3. Ve a la sección "API Keys"
4. Genera una nueva API Key
5. Copia la clave (empieza con `sk-ant-api03-...`)

## Paso 2: Configurar la Aplicación

1. Abre el archivo `appsettings.json`
2. Reemplaza `TU_API_KEY_AQUI` con tu API Key:

```json
"Anthropic": {
  "ApiKey": "sk-ant-api03-TU_CLAVE_REAL_AQUI"
}
```

3. Guarda el archivo

## Paso 3: Ejecutar la Aplicación

### En Windows (PowerShell):
```powershell
cd AnalizadorSoftware
dotnet restore
dotnet run
```

### En Linux/Mac:
```bash
cd AnalizadorSoftware
dotnet restore
dotnet run
```

## Paso 4: Abrir en el Navegador

La aplicación estará disponible en:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000

## Paso 5: Probar la Aplicación

1. Verás un formulario con un cuadro de texto grande
2. Copia uno de los ejemplos del archivo `EJEMPLOS.md`
3. Pégalo en el cuadro de texto
4. Haz clic en "✨ Analizar con IA"
5. Espera unos segundos mientras la IA procesa la especificación
6. ¡Verás los resultados con procesos, subprocesos y casos de uso!

## 🎯 Ejemplo Rápido para Probar

Copia y pega esto en el formulario:

```
Necesito un sistema de gestión de tareas que permita:
- Crear, editar y eliminar tareas
- Asignar prioridades (alta, media, baja)
- Establecer fechas de vencimiento
- Marcar tareas como completadas
- Filtrar tareas por estado y prioridad
- Recibir notificaciones de tareas próximas a vencer
```

## 📊 Verificar la Base de Datos

Después de analizar algunas especificaciones, se creará el archivo `analizador.db` en la carpeta del proyecto. Puedes abrirlo con herramientas como:

- **DB Browser for SQLite**: https://sqlitebrowser.org/
- **SQLiteStudio**: https://sqlitestudio.pl/

## ⚠️ Solución de Problemas

### Error: "No se puede conectar a la API de Anthropic"
- Verifica que tu API Key sea correcta
- Asegúrate de tener conexión a Internet

### Error: "The type or namespace name 'Microsoft' could not be found"
- Ejecuta: `dotnet restore`

### La página no carga
- Verifica que el puerto 5001 (HTTPS) o 5000 (HTTP) no esté en uso
- Si necesitas cambiar el puerto, edita `Properties/launchSettings.json`

## 💰 Costos de la API

Ten en cuenta que usar la API de Claude tiene un costo:
- Claude Sonnet 4: Aproximadamente $3 por millón de tokens de entrada

Cada análisis usa aproximadamente 1,000-2,000 tokens, así que el costo por análisis es de unos pocos centavos.

## 📚 Recursos Adicionales

- Documentación de la API de Claude: https://docs.anthropic.com/
- Documentación de .NET: https://docs.microsoft.com/dotnet/
- Entity Framework Core: https://docs.microsoft.com/ef/core/

## 🎉 ¡Listo!

Ahora tienes una aplicación funcional que usa IA para analizar especificaciones de software y generar automáticamente la documentación técnica.
