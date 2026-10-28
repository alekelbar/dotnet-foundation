# Arquitectura de la aplicación

Esta guía define convenciones generales para organizar código nuevo. Su lectura
es obligatoria antes de agregar o modificar código de aplicación. El alcance y
la regla de mantenimiento de las guías se definen en [AGENTS.md](../AGENTS.md).
Las decisiones de cada funcionalidad corresponden a su spec.

La aplicación se divide en cuatro proyectos por capa. Cada proyecto organiza su
código por feature. Los identificadores, namespaces, tablas y rutas se escriben
en inglés; los textos de interfaz se escriben en español.

## Estructura y ubicación de responsabilidades

```text
AppTemplate.slnx
src/
  AppTemplate.Domain/
    Features/<Feature>/
      Models/
      Services/
  AppTemplate.Application/
    Features/<Feature>/
      Models/
      Services/
      Repositories/
      Helpers/
  AppTemplate.Infrastructure/
    DependencyInjection.cs
    Data/
      AppDbContext.cs
      Migrations/
    Features/<Feature>/
      Configurations/
      Repositories/
  AppTemplate.Presentation/
    Program.cs
    Components/
    Features/<Feature>/
      _Imports.razor
      Pages/
      Components/
      Services/
      Models/
      Helpers/
    wwwroot/
```

El namespace sigue `AppTemplate.<Layer>.Features.<Feature>.<Responsibility>`.
La persistencia compartida usa `AppTemplate.Infrastructure.Data`. El proyecto ya
representa la capa: no repitas carpetas Domain/Application dentro de una feature.
No coloques entidades, servicios o configuraciones sueltos en su raíz.

En la tabla siguiente, las carpetas por responsabilidad se ubican dentro de
`Features/<Feature>/`, salvo las rutas compartidas indicadas expresamente.

| Tipo de código | Ubicación y límite |
| --- | --- |
| Entidades y enums de negocio | Domain, `Models/`; sin atributos de EF o UI. |
| Reglas de negocio | Domain, `Services/`; reciben los datos y fechas necesarios, sin reloj ni acceso a datos. |
| Entrada y salida públicas | Application, `Models/`; no exponen entidades persistidas ni tipos EF. |
| Servicios de aplicación | Application, `Services/`; coordinan por responsabilidad, sin una clase obligatoria por operación. |
| Contratos de persistencia | Application, `Repositories/`; específicos de la feature, pueden usar Domain, sin `IQueryable` ni EF. |
| Mapeo y validación de contratos | Application, `Helpers/`; mantienen explícita la conversión entre contratos y dominio y reutilizan las reglas de Domain. |
| Repositorios EF y mapeo de tablas | Infrastructure, `Repositories/` y `Configurations/`. |
| Contexto y migraciones | Infrastructure, `Data/`; compartidos por las features. |
| Estado mutable de presentación | Presentation, `Models/`; separado de entradas, DTOs y entidades. |
| Páginas y diálogos | Presentation, `Pages/` y `Components/`; consumen servicios de Application. |
| Integración con el navegador | Presentation, `Services/` y `wwwroot/`; las capas internas no conocen APIs ni almacenamiento del navegador. |
| Etiquetas españolas | Presentation, `Helpers/`; separadas de las reglas de negocio. |
| Host, layout y tema | Presentation, `Program.cs` y `Components/`; estilos según [la guía de diseño](DESIGN.md). |

Los enums de contrato son independientes de los del dominio y se convierten
explícitamente. Los formularios no modifican directamente DTOs ni entidades.
La validación de Application reutiliza las reglas de Domain sin depender de la UI.

## Dependencias permitidas y límites

| Proyecto | Referencias directas permitidas | Dependencias externas |
| --- | --- | --- |
| Domain | Ninguna | Ninguna |
| Application | Domain | Ninguna |
| Infrastructure | Application y Domain | EF Core SQLite |
| Presentation | Application e Infrastructure | MudBlazor; EF Core Design con `PrivateAssets=all` |

Domain y Application no usan EF, SQLite, Blazor, MudBlazor o tipos del host.
Presentation no añade una referencia directa a Domain. Su referencia a
Infrastructure es una excepción para la composición e inicialización en
`Program.cs`: páginas, componentes y auxiliares no la consumen. La UI usa
contratos y servicios de Application, sin acceder a repositorios o EF.

Los límites se mantienen mediante referencias, visibilidad y revisión de usos:

1. Las capas internas compilan sin referencias a las externas. No agregues
   referencias inversas para resolver un error de compilación: coloca el
   contrato en la capa que lo necesita.
2. Contexto, configuraciones, repositorios EF, mapeos y auxiliares locales son
   `internal`. Las interfaces, contratos y reglas que cruzan ensamblados son
   públicos. Los servicios concretos de Application son públicos porque el host
   los registra; la UI inyecta sus interfaces.
3. .NET resuelve referencias transitivas. El host puede alcanzar tipos públicos
   de Domain o puertos de repositorio por esa vía; tampoco `PrivateAssets`
   impide usar tipos de diseño desde el propio host. Revisa los imports y tipos
   usados por Presentation: la compilación no impone una barrera absoluta.

El paquete EF de diseño permanece en el proyecto de arranque para las
herramientas, con activos privados. No justifica lógica de persistencia en
Presentation y no se referencia desde las capas internas.

## Composición y persistencia compartida

`src/AppTemplate.Presentation/Program.cs` compone el host: registra los servicios
de Application y consume la entrada pública de Infrastructure. Las
implementaciones EF se registran desde su propio ensamblado mediante
`src/AppTemplate.Infrastructure/DependencyInjection.cs`.

La inicialización aplica migraciones antes de atender solicitudes. Ante un fallo,
el host registra el error crítico y detiene el arranque; no elimina ni recrea
bases como recuperación.

El contexto y las migraciones son compartidos dentro de Infrastructure. Las
rutas relativas de conexión se resuelven contra la raíz de contenido del host.
Los cambios de ubicación o namespace deben conservar tablas, columnas, claves e
identificadores de migraciones y ajustar coherentemente snapshot y archivos de
diseño. No se modifica el comportamiento histórico de `Up` y `Down` ni se genera
una migración de esquema solo por un traslado.

Añade migraciones únicamente para cambios reales de esquema. No reinicies el
historial de migraciones ni borres una base para resolver un error de modelo o
arranque.

## Incorporar otra feature

1. Identifica sus entidades y reglas puras en Domain; crea únicamente las
   carpetas que contengan código real.
2. Define entradas, DTOs, servicios por responsabilidad y puertos específicos en
   Application. Mantén los contratos de UI separados de las entidades.
3. Implementa persistencia y configuraciones en Infrastructure. Integra sus
   entidades en el contexto compartido y registra implementaciones internas
   desde Infrastructure.
4. Incorpora páginas, componentes internos, formularios y etiquetas en
   Presentation. Limita los imports Razor a su feature e inyecta servicios de
   Application. Registra esos servicios en la composición del host.
5. Revisa las referencias, la visibilidad y la separación de responsabilidades
   conforme a esta guía.

No crees cuatro proyectos por cada feature ni carpetas vacías preventivas.
`Models/`, `Services/`, `Repositories/`, `Helpers/`, `Configurations/`, `Pages/` y
`Components/` se usan solo donde tienen contenido. Un helper debe tener una
responsabilidad concreta, como mapear contratos o producir etiquetas; evita
`CommonHelper` o utilidades genéricas sin dueño funcional.

No agregues repositorio genérico, Unit of Work genérico, CQRS, MediatR, bus de
eventos, librería de mapeo o una clase por operación sin una necesidad acordada.
Evita dependencias directas entre componentes internos de features distintas.
