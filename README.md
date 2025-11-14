# Analizador de Especificaciones de Software

Aplicación web .NET que utiliza IA (Claude) para analizar especificaciones de software y generar automáticamente procesos, subprocesos y casos de uso que se almacenan en una base de datos SQLite.

## 🚀 Características

- **Análisis con IA**: Usa la API de Claude para analizar especificaciones de software
- **Generación automática**: Crea procesos, subprocesos y casos de uso basados en la especificación
- **Almacenamiento**: Guarda los resultados en una base de datos SQLite
- **Interfaz moderna**: UI limpia y fácil de usar
- **Historial**: Visualiza los últimos análisis realizados

## 📋 Requisitos

- .NET 8.0 SDK
- API Key de Anthropic (Claude)

## 🔧 Configuración

1. **Clonar o descargar el proyecto**

2. **Configurar la API Key de Anthropic**
   
   Edita el archivo `appsettings.json` y reemplaza `TU_API_KEY_AQUI` con tu API Key:
   
   ```json
   "Anthropic": {
     "ApiKey": "sk-ant-api03-..."
   }
   ```

3. **Restaurar paquetes NuGet**
   
   ```bash
   dotnet restore
   ```

4. **Ejecutar la aplicación**
   
   ```bash
   dotnet run
   ```

5. **Abrir en el navegador**
   
   La aplicación estará disponible en: `https://localhost:5001` o `http://localhost:5000`

## 💡 Uso

1. Ingresa la especificación de tu software en el cuadro de texto
2. Haz clic en "✨ Analizar con IA"
3. La IA analizará tu especificación y generará:
   - Procesos principales
   - Subprocesos para cada proceso
   - Casos de uso detallados con actores, precondiciones, postcondiciones, etc.
4. Los resultados se guardarán automáticamente en la base de datos SQLite

## 📊 Estructura de la Base de Datos

### Tabla: proceso
- `id_proceso`: ID autoincremental
- `nombre`: Nombre del proceso
- `descripcion`: Descripción del proceso

### Tabla: subproceso
- `id_subproceso`: ID autoincremental
- `id_proceso`: FK a proceso
- `nombre`: Nombre del subproceso
- `descripcion`: Descripción del subproceso

### Tabla: caso_uso
- `id_caso_uso`: ID autoincremental
- `id_subproceso`: FK a subproceso
- `nombre`: Nombre del caso de uso
- `descripcion`: Descripción del caso de uso
- `actor_principal`: Actor principal del caso de uso
- `tipo_caso_uso`: 1=Funcional, 2=No Funcional, 3=Sistema
- `precondiciones`: Precondiciones del caso
- `postcondiciones`: Postcondiciones del caso
- `criterios_de_aceptacion`: Criterios de aceptación

## 🗂️ Estructura del Proyecto

```
AnalizadorSoftware/
├── Data/
│   └── AppDbContext.cs          # Contexto de Entity Framework
├── Models/
│   └── Entidades.cs             # Entidades del modelo de datos
├── Pages/
│   ├── Index.cshtml             # Vista principal
│   ├── Index.cshtml.cs          # Code-behind de la página
│   └── _ViewImports.cshtml      # Imports de Razor
├── Services/
│   └── AnalizadorService.cs     # Servicio de análisis con IA
├── Program.cs                    # Configuración de la aplicación
├── appsettings.json             # Configuración (API Key, DB)
└── AnalizadorSoftware.csproj    # Archivo del proyecto
```

## 📝 Ejemplo de Especificación

```
Necesito un sistema de gestión de biblioteca que permita:
- Registrar libros con ISBN, título, autor, editorial
- Gestionar préstamos de libros a usuarios
- Controlar las fechas de devolución
- Generar multas por retrasos
- Buscar libros por diferentes criterios
- Reservar libros que estén prestados
```

## 🔒 Nota de Seguridad

⚠️ **Importante**: No subas tu API Key a repositorios públicos. Considera usar variables de entorno o Azure Key Vault para producción.

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.
