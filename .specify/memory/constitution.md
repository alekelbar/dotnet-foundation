<!--
Sync Impact Report — material temporal de revisión; retirar antes del commit.
Versión: sin versión (scaffold) → 1.0.0; primera adopción de la constitución.
Principios: los cinco marcadores se sustituyen por:
  I. Arquitectura por capas y funcionalidades
  II. Límites explícitos entre capas
  III. Diseño compartido
  IV. Accesibilidad e interacción
  V. Código en inglés e idioma de interfaz por especificación
Secciones incorporadas: Fuentes de convenciones; Revisión de cumplimiento; Governance.
Secciones eliminadas: ninguna sección normativa; se retiran ejemplos y marcadores.
Contradicciones con las indicaciones del usuario: ninguna en las fuentes consultadas.
Pendientes y TODOs: ninguno. Plantillas y comandos dependientes sin modificaciones.
-->

# AppTemplate Constitution

## Core Principles

### I. Arquitectura por capas y funcionalidades

El código DEBE organizarse por funcionalidad dentro de Domain, Application,
Infrastructure y Presentation, con las responsabilidades y dependencias definidas en
[la guía de arquitectura](../../docs/architecture.md). Las reglas de negocio pertenecen
a Domain; la coordinación a Application; la persistencia a Infrastructure; y la interfaz
a Presentation. Esta separación mantiene cada responsabilidad en su capa.

### II. Límites explícitos entre capas

Domain y Application DEBEN permanecer independientes de persistencia, UI y host.
La UI DEBE consumir contratos y servicios de Application, sin acceder a repositorios,
EF ni entidades persistidas. El uso de Infrastructure desde Presentation DEBE limitarse
a composición e inicialización en `Program.cs`. Los límites DEBEN revisarse también en
los tipos e imports utilizados, porque las referencias transitivas no los garantizan.

### III. Diseño compartido

La UI DEBE reutilizar MudBlazor, el tema y los tokens compartidos según
[la guía de diseño](../../docs/DESIGN.md). Los componentes y pantallas NO DEBEN definir
paletas, familias tipográficas, sombras ni radios locales. Los estilos de composición
DEBEN consumir los tokens compartidos para mantener una apariencia coherente.

### IV. Accesibilidad e interacción

La UI DEBE preservar navegación por teclado, foco visible y ordenado, nombres accesibles
para acciones de solo icono y restauración del foco al cerrar diálogos. DEBE respetar
`prefers-reduced-motion`, comunicar estados sin depender solo del color y cumplir el
contraste WCAG AA indicado en la guía de diseño. Los controles y diálogos DEBEN seguir
siendo utilizables a 320 px sin desplazamiento horizontal.

### V. Código en inglés e idioma de interfaz por especificación

El código, sus identificadores, comentarios y documentación interna DEBEN estar en inglés,
al igual que los nombres de entidades, tablas, columnas y rutas de aplicación. Los
documentos pueden estar en español; sus fragmentos de código DEBEN estar en inglés.
Cada especificación DEBE determinar el idioma de su interfaz de usuario.

## Fuentes de convenciones

[AGENTS.md](../../AGENTS.md), [docs/architecture.md](../../docs/architecture.md) y
[docs/DESIGN.md](../../docs/DESIGN.md) conservan las convenciones detalladas. Esta
constitución sintetiza sus principios; no sustituye ni replica sus catálogos, estructuras
o valores. Antes de modificar código o UI se DEBE leer la guía correspondiente.

Las decisiones y flujos de cada funcionalidad pertenecen a su especificación. Modificar
esta constitución o ejecutar una especificación NO autoriza cambios en `AGENTS.md` ni
en archivos bajo `docs/`: requieren una solicitud explícita dirigida a esos archivos.

## Revisión de cumplimiento

La revisión de especificaciones, planes y cambios DEBE comprobar la ubicación de
responsabilidades, las referencias y usos entre capas, el consumo del diseño compartido,
la accesibilidad y las convenciones de idioma que correspondan. Los detalles se
contrastan con las guías enlazadas, sin convertirlas en inventarios de funcionalidades.
Toda contradicción detectada con las indicaciones del usuario DEBE señalarse; no habilita
a modificar las guías protegidas ni a introducir restricciones sin consultarle.

## Governance

Las enmiendas DEBEN indicar qué principios cambian y por qué, actualizar la versión y la
fecha de modificación, y revisar su coherencia con las fuentes de convenciones.
Las nuevas restricciones DEBEN consultarse con el usuario antes de incorporarlas.

La versión sigue SemVer: MAJOR para eliminar o redefinir principios de forma incompatible;
MINOR para añadir principios o ampliar materialmente su alcance; PATCH para aclaraciones
sin cambio de obligaciones. Las revisiones DEBEN comprobar el cumplimiento de la versión
vigente. La fecha de ratificación registra la primera adopción y se conserva en enmiendas.

**Version**: 1.0.0 | **Ratified**: 2026-11-03 | **Last Amended**: 2026-11-03
