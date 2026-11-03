# Application architecture

This guide defines general conventions for organizing new code. Read it before
adding or modifying application code. The scope and maintenance rule for the
guides are defined in [AGENTS.md](../AGENTS.md). Feature decisions belong in
their respective specs.

The application is divided into four projects, one per layer. Each project
organizes its code by feature. Identifiers, namespaces, tables, and routes must
be in English; interface text must be in Spanish.

## Structure and responsibility placement

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

Namespaces follow `AppTemplate.<Layer>.Features.<Feature>.<Responsibility>`.
Shared persistence uses `AppTemplate.Infrastructure.Data`. The project already
represents the layer: do not repeat Domain/Application folders inside a feature.
Do not place loose entities, services, or configurations at the feature root.

In the following table, responsibility folders are located inside
`Features/<Feature>/`, except for the explicitly indicated shared paths.

| Code type | Location and boundary |
| --- | --- |
| Business entities and enums | Domain, `Models/`; no EF or UI attributes. |
| Business rules | Domain, `Services/`; receive the required data and dates, without clock or data access. |
| Public inputs and outputs | Application, `Models/`; do not expose persisted entities or EF types. |
| Application services | Application, `Services/`; coordinate by responsibility, without requiring one class per operation. |
| Persistence contracts | Application, `Repositories/`; feature-specific, may use Domain, without `IQueryable` or EF. |
| Contract mapping and validation | Application, `Helpers/`; keep conversions between contracts and domain explicit and reuse Domain rules. |
| EF repositories and table mappings | Infrastructure, `Repositories/` and `Configurations/`. |
| Context and migrations | Infrastructure, `Data/`; shared across features. |
| Mutable presentation state | Presentation, `Models/`; separate from inputs, DTOs, and entities. |
| Pages and dialogs | Presentation, `Pages/` and `Components/`; consume Application services. |
| Browser integration | Presentation, `Services/` and `wwwroot/`; inner layers have no knowledge of browser APIs or storage. |
| Spanish labels | Presentation, `Helpers/`; separate from business rules. |
| Host, layout, and theme | Presentation, `Program.cs` and `Components/`; styles follow the [design guide](DESIGN.md). |

Contract enums are independent of domain enums and are converted explicitly.
Forms do not modify DTOs or entities directly. Application validation reuses
Domain rules without depending on the UI.

## Allowed dependencies and boundaries

| Project | Allowed direct references | External dependencies |
| --- | --- | --- |
| Domain | None | None |
| Application | Domain | None |
| Infrastructure | Application and Domain | EF Core SQLite |
| Presentation | Application and Infrastructure | MudBlazor; EF Core Design with `PrivateAssets=all` |

Domain and Application do not use EF, SQLite, Blazor, MudBlazor, or host types.
Presentation does not add a direct reference to Domain. Its reference to
Infrastructure is an exception for composition and initialization in `Program.cs`:
pages, components, and helpers do not consume it. The UI uses Application
contracts and services without accessing repositories or EF.

Boundaries are maintained through references, visibility, and usage review:

1. Inner layers compile without references to outer layers. Do not add reverse
   references to resolve a compilation error: place the contract in the layer
   that needs it.
2. The context, configurations, EF repositories, mappings, and local helpers are
   `internal`. Interfaces, contracts, and rules that cross assembly boundaries
   are public. Concrete Application services are public because the host
   registers them; the UI injects their interfaces.
3. .NET resolves transitive references. The host can reach public Domain types
   or repository ports through them; `PrivateAssets` also does not prevent the
   host itself from using design-time types. Review the imports and types used
   by Presentation: compilation does not enforce an absolute boundary.

The EF design-time package remains in the startup project for tooling, with
private assets. It does not justify persistence logic in Presentation and is
not referenced from inner layers.

## Composition and shared persistence

`src/AppTemplate.Presentation/Program.cs` composes the host: it registers
Application services and consumes Infrastructure's public entry point.
EF implementations are registered from their own assembly through
`src/AppTemplate.Infrastructure/DependencyInjection.cs`.

Initialization applies migrations before serving requests. On failure, the host
logs the critical error and stops startup; it does not delete or recreate
databases as a recovery mechanism.

The context and migrations are shared within Infrastructure. Relative connection
paths are resolved against the host's content root. Location or namespace
changes must preserve tables, columns, keys, and migration identifiers, and
consistently update the snapshot and designer files. Do not modify the historical
behavior of `Up` and `Down` or generate a schema migration solely for relocation.

Add migrations only for actual schema changes. Do not reset migration history
or delete a database to resolve a model or startup error.

## Adding another feature

1. Identify its entities and pure rules in Domain; create only folders that
   contain actual code.
2. Define inputs, DTOs, services by responsibility, and feature-specific ports
   in Application. Keep UI contracts separate from entities.
3. Implement persistence and configurations in Infrastructure. Integrate its
   entities into the shared context and register internal implementations
   from Infrastructure.
4. Add pages, internal components, forms, and labels in Presentation. Limit
   Razor imports to their feature and inject Application services. Register
   those services in the host composition.
5. Review references, visibility, and separation of responsibilities according
   to this guide.

Do not create four projects per feature or empty folders in anticipation of
future needs. Use `Models/`, `Services/`, `Repositories/`, `Helpers/`,
`Configurations/`, `Pages/`, and `Components/` only where they contain content.
A helper must have a specific responsibility, such as mapping contracts or
producing labels; avoid `CommonHelper` or generic utilities without a functional
owner.

Do not add a generic repository, generic Unit of Work, CQRS, MediatR, event bus,
mapping library, or one class per operation without an agreed need. Avoid direct
dependencies between internal components of different features.
