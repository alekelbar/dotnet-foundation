# Aplicación base

Plantilla mínima de .NET 10 con Blazor Interactive Server, MudBlazor y cuatro
capas organizadas por feature. Conserva las convenciones y la apariencia de la
prueba de concepto original, sin su funcionalidad de hábitos.

## Ejecutar

Requisito: SDK de .NET 10. No requiere Node.js ni un servidor de base de datos.
Desde esta carpeta:

```sh
dotnet restore AppTemplate.slnx
dotnet build AppTemplate.slnx --no-restore
dotnet run --project src/AppTemplate.Presentation --launch-profile http
```

Abre http://localhost:5059. El botón **Probar interacción** muestra una notificación
para comprobar que el circuito interactivo funciona. El perfil `https` usa
https://localhost:7052 y requiere un certificado de desarrollo de ASP.NET Core.

SQLite usa `application.db` en la raíz del host de presentación. El arranque
conserva la inicialización mediante migraciones y aborta si falla. La base no
incluye entidades, tablas de negocio ni migraciones del proyecto original; EF
puede crear su historial de migraciones al iniciar. Domain y Application están
intencionalmente vacíos hasta incorporar una funcionalidad real.

## Estructura

```text
AppTemplate.slnx
AGENTS.md
docs/
  architecture.md
  DESIGN.md
specs/
references/
src/
  AppTemplate.Domain/
  AppTemplate.Application/
  AppTemplate.Infrastructure/
    Data/AppDbContext.cs
    DependencyInjection.cs
  AppTemplate.Presentation/
    Components/
    wwwroot/
```

Referencias: Application → Domain; Infrastructure → Application y Domain;
Presentation → Application e Infrastructure. Consulta
[arquitectura](docs/architecture.md) antes de añadir código y
[DESIGN.md](docs/DESIGN.md) antes de cambiar la interfaz.

La guía de arquitectura conserva las responsabilidades, límites, composición y
reglas de persistencia originales con nombres neutrales. El diseño conserva la
paleta y convenciones del tema, expresadas con las ocho secciones y tokens YAML
del formato oficial de Google Stitch. No hay generación automática de estilos
ni conexión a un proyecto Stitch.

## Usar como plantilla de dotnet

También se incluye una definición para generar proyectos con otro nombre sin
renombrar manualmente ensamblados y namespaces. No es necesario instalarla para
ejecutar esta base.

```sh
dotnet new install .
dotnet new blazor-layered -n MyApplication -o ../my-application
```

El generador reemplaza `AppTemplate` en nombres de archivos y contenido. Cada
aplicación usa los mismos puertos de desarrollo por defecto: cámbialos en
`Properties/launchSettings.json` para ejecutar varias a la vez.

## Agregar la primera funcionalidad

Crea únicamente carpetas con código real bajo `Features/<Feature>` en cada capa
que lo necesite. Define reglas en Domain, contratos y servicios en Application,
persistencia en Infrastructure e interfaz en Presentation. Agrega los conjuntos
de entidades y sus configuraciones al contexto compartido de Infrastructure. Registra los servicios
de Application en `Program.cs` y las implementaciones de persistencia en
`DependencyInjection.cs`.

Cuando incorpores entidades, crea la primera migración del nuevo proyecto:

```sh
dotnet ef migrations add InitialSchema --project src/AppTemplate.Infrastructure --startup-project src/AppTemplate.Presentation --output-dir Data/Migrations
```

Este comando requiere la herramienta `dotnet-ef` 10.x. No reutilices migraciones
ni bases de datos de la prueba de concepto. A partir de la primera migración,
conserva el historial y no uses `EnsureCreated` para reemplazarlo.

## Especificaciones y referencias

`specs/` y `references/` comienzan vacíos, salvo por un `.gitkeep` para conservar
ambos directorios en Git. Usa `specs/` para las especificaciones del nuevo proyecto
y `references/` para su material de referencia.

Se excluyen el repositorio Git original, sus artefactos de compilación, bases
locales, entidades, servicios, pantallas, scripts y migraciones de hábitos.
