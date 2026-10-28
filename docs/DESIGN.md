---
version: alpha
name: Application Base
description: Calm light interface with blue actions, local typography and restrained depth.
colors:
  primary: "#345BD6"
  on-primary: "#FFFFFF"
  background: "#F5F7FA"
  surface: "#FFFFFF"
  on-surface: "#243247"
  secondary: "#637187"
  outline: "#E5EAF1"
  input-border: "#C6CFDD"
  primary-container: "#EDF1FF"
  empty-container: "#EFF3FC"
  hover-container: "#E9EDF5"
  selection-container: "#E9EDF3"
  selection-text: "#526078"
  success: "#28745B"
  success-container: "#EDF6F1"
  inactive-container: "#F0F2F6"
  error: "#B43F4B"
typography:
  page-title:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 3rem
    fontWeight: 400
    lineHeight: 1.15
  dialog-title:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 1.4rem
    fontWeight: 500
    lineHeight: 1.4
  empty-title:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 1.15rem
    fontWeight: 500
    lineHeight: 1.4
  body:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 1rem
    fontWeight: 400
    lineHeight: 1.5
  body-compact:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 0.9rem
    fontWeight: 400
    lineHeight: 1.5
  label:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 0.85rem
    fontWeight: 500
    lineHeight: 1.5
  caption:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 0.8rem
    fontWeight: 400
    lineHeight: 1.5
  status:
    fontFamily: Avenir Next, Avenir, Segoe UI, sans-serif
    fontSize: 0.75rem
    fontWeight: 400
    lineHeight: 1.5
spacing:
  compact: 2px
  tight: 4px
  control: 8px
  inline: 12px
  group: 16px
  section: 20px
  card: 24px
  dialog: 28px
  content-gutter: 36px
  content-gutter-mobile: 20px
  content-width: 1120px
rounded:
  status: 6px
  control-small: 7px
  control: 8px
  default: 10px
  list: 16px
  dialog: 18px
  empty-icon: 22px
components:
  button-primary:
    backgroundColor: "{colors.primary}"
    textColor: "{colors.on-primary}"
    rounded: "{rounded.default}"
  button-secondary:
    backgroundColor: transparent
    textColor: "{colors.primary}"
    rounded: "{rounded.default}"
  input:
    backgroundColor: "{colors.surface}"
    textColor: "{colors.on-surface}"
    rounded: "{rounded.default}"
  dialog:
    backgroundColor: "{colors.surface}"
    textColor: "{colors.on-surface}"
    rounded: "{rounded.dialog}"
    padding: "{spacing.dialog}"
  empty-state:
    backgroundColor: "{colors.empty-container}"
    textColor: "{colors.secondary}"
    rounded: "{rounded.list}"
    padding: "{spacing.card}"
---

# Sistema de diseño

## Overview

Interfaz clara, tranquila y espaciosa para aplicaciones de uso cotidiano. El
contenido se lee de izquierda a derecha, con jerarquía tipográfica visible y una
acción principal fácil de reconocer. Usa fondo gris frío, superficies blancas y
acento azul. El tema es siempre claro, independientemente del sistema operativo.
No presupone una actividad, entidad de negocio o sector específico.

Esta guía define convenciones generales; la composición de cada pantalla y sus
recorridos corresponden a su spec. La regla de mantenimiento está en
[AGENTS.md](../AGENTS.md). El formato sigue la
[especificación oficial DESIGN.md de Google Stitch](https://github.com/google-labs-code/design.md/blob/main/docs/spec.md)
(versión alpha, consultada el 27 de octubre de 2026).

## Colors

- Azul `primary`: acción principal, selección y foco; texto blanco sobre botones rellenos.
- `background`: lienzo general. `surface`: barras, controles y contenedores.
- `on-surface`: texto principal. `secondary`: descripciones y metadatos.
- `outline`: divisores; `input-border`: contornos de campos.
- `primary-container` y `empty-container`: fondos suaves para énfasis y estados vacíos.
- `hover-container`: interacción al pasar el puntero.
- `success` y `error`: resultados correctos y acciones destructivas o errores.
  Siempre acompaña el color con texto o iconos.

La implementación configura roles de MudBlazor en
`src/AppTemplate.Presentation/Components/Theming/AppTheme.cs`.
`Create()` entrega un tema nuevo para cada instancia del layout: no compartas un
`MudTheme` mutable entre circuitos.
`src/AppTemplate.Presentation/wwwroot/theme.css` contiene alias como
`--app-accent: var(--mud-palette-primary)` y valores visuales complementarios.
No copies colores de roles MudBlazor en CSS ni crees paletas locales.

Los tokens YAML documentan los valores de referencia; no generan automáticamente
C# ni CSS. Al cambiar valores, mantén sincronizados esta guía y sus puntos de
configuración cuando la actualización de la guía esté autorizada.

## Typography

Usa la familia local **Avenir Next**, con alternativas **Avenir**, **Segoe UI** y
`sans-serif`. No descargues fuentes remotas. La apariencia concreta puede variar
según las fuentes instaladas.

El título de página es fluido: `clamp(2.25rem, 5vw, 3rem)`; el token YAML expresa
su tamaño máximo. Las restantes escalas están en el front matter. MudBlazor
hereda la familia desde `AppTheme.cs`; los estilos de composición consumen los
tokens `--app-type-*` según su función. Evita recortar títulos y nombres largos.
Textos y acciones en español, en estilo oración, sin mayúsculas forzadas por CSS.

## Layout

Contenido fluido con máximo de **1120 px**, centrado y alineado a la izquierda.
Margen interior de **36 px** en escritorio y **20 px** hasta 600 px de ancho.
Usa la escala de espaciados según la relación entre elementos: controles,
grupos, secciones y contenedores. Mantén los controles y diálogos utilizables a
320 px sin desplazamiento horizontal.

`wwwroot/app.css` y estilos aislados describen composición y consumen tokens.
Los valores estructurales como `0`, porcentajes, posiciones, breakpoints y
geometría de animaciones pueden permanecer en esas hojas. Mantén los estilos de
cada feature junto a su responsabilidad; evita acoplar componentes internos de
features diferentes.

## Elevation & Depth

Construye jerarquía con espacio, superficies y divisores. Evita gradientes y
sombras repetidas en filas. Reserva `--app-dialog-shadow`
(`0 24px 80px #24324726`) para diálogos y `--app-selected-shadow`
(`0 1px 3px #2432470d`) para selección cuando sea necesaria.
El aviso de error y la reconexión tienen tokens compartidos específicos para sus
sombras y fondos. No definas sombras locales en componentes.

## Shapes

Controles MudBlazor con radio predeterminado de **10 px**. La escala complementaria
incluye controles de 8 px, estados de 6 px, listas de 16 px, diálogos de 18 px e
iconos de estado vacío de 22 px. Selecciona el radio por función y reutiliza
`--app-radius-*`; no inventes radios por pantalla.

## Components

Reutiliza MudBlazor para botones, campos delineados, selectores y diálogos.
Prioriza una acción principal; distingue las destructivas mediante el rol de
error. En diálogos separa acciones y contenido, con cancelar antes de confirmar.
Los tokens de componentes describen sus variantes visuales; usa los tokens
compartidos al implementar un componente que todavía no exista en la base.

Define carga, vacío, error, selección y deshabilitado cuando el flujo lo requiera.
Conserva señales de interacción y deshabilitado, foco visible y nombres accesibles
en acciones que solo muestran iconos. Mantén el orden del foco y su restauración
al cerrar un diálogo. Respeta `prefers-reduced-motion`.

`Components/App.razor` carga MudBlazor, `theme.css`, `app.css` y estilos aislados,
en ese orden. Los tokens se declaran en `:root` para alcanzar también popovers y
diálogos fuera del contenido. Conserva solo los overrides globales necesarios y
comenta su motivo; no agregues familias, paletas, radios ni sombras locales.

## Do's and Don'ts

- Reutiliza roles semánticos y tokens compartidos. Agrega nuevos tokens por función,
  en inglés, con prefijo `--app-`, una sola vez en `theme.css`.
- Mantén una acción principal clara y mensajes concretos.
- Comprueba contraste WCAG AA: 4.5:1 para texto normal, 3:1 para texto grande y
  señales visuales esenciales de controles. Revisa también hover, foco y error.
- Revisa teclado, texto largo y móvil después de cambiar el tema.
- No dependas solo del color para comunicar estados.
- No agregues fuentes remotas, gradientes decorativos o animaciones automáticas.
- No dupliques valores del tema en hojas de composición. `transparent` y
  `currentColor` pueden usarse como valores estructurales.
- No conviertas esta guía en un inventario de funcionalidades o historial de
  decisiones: esos detalles pertenecen a sus specs.
