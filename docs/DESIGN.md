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

# Design system

## Overview

A clear, calm, and spacious interface for everyday applications. Content reads
from left to right, with a visible typographic hierarchy and an easily recognized
primary action. Use a cool gray background, white surfaces, and a blue accent.
The theme is always light, regardless of the operating system. It does not assume
a specific activity, business entity, or industry.

This guide defines general conventions; each screen's composition and flows
belong in its spec. The maintenance rule is in [AGENTS.md](../AGENTS.md).
The format follows the
[official Google Stitch DESIGN.md specification](https://github.com/google-labs-code/design.md/blob/main/docs/spec.md)
(alpha version, accessed October 27, 2026).

## Colors

- Blue `primary`: primary action, selection, and focus; white text on filled buttons.
- `background`: overall canvas. `surface`: bars, controls, and containers.
- `on-surface`: primary text. `secondary`: descriptions and metadata.
- `outline`: dividers; `input-border`: field outlines.
- `primary-container` and `empty-container`: subtle backgrounds for emphasis and empty states.
- `hover-container`: interaction on pointer hover.
- `success` and `error`: successful outcomes and destructive actions or errors.
  Always accompany color with text or icons.

The implementation configures MudBlazor roles in
`src/AppTemplate.Presentation/Components/Theming/AppTheme.cs`.
`Create()` returns a fresh theme for each layout instance: do not share a mutable
`MudTheme` across circuits.
`src/AppTemplate.Presentation/wwwroot/theme.css` contains aliases such as
`--app-accent: var(--mud-palette-primary)` and complementary visual values.
Do not copy MudBlazor role colors into CSS or create local palettes.

YAML tokens document reference values; they do not automatically generate C# or
CSS. When changing values, keep this guide and its configuration points in sync
when updating the guide is authorized.

## Typography

Use the local **Avenir Next** family, with **Avenir**, **Segoe UI**, and
`sans-serif` as fallbacks. Do not download remote fonts. The actual appearance
may vary depending on the installed fonts.

The page title is fluid: `clamp(2.25rem, 5vw, 3rem)`; the YAML token expresses
its maximum size. The remaining scales are in the front matter. MudBlazor
inherits the family from `AppTheme.cs`; composition styles consume
`--app-type-*` tokens according to their purpose. Avoid clipping long titles
and names. Text and actions must be in Spanish, in sentence case, without
CSS-enforced capitalization.

## Layout

Fluid content with a maximum width of **1120 px**, centered with left-aligned
content. Use **36 px** of padding on desktop and **20 px** at widths up to 600 px.
Use the spacing scale according to the relationship between elements: controls,
groups, sections, and containers. Keep controls and dialogs usable at 320 px
without horizontal scrolling.

`wwwroot/app.css` and isolated styles describe composition and consume tokens.
Structural values such as `0`, percentages, positions, breakpoints, and animation
geometry may remain in those stylesheets. Keep each feature's styles alongside
its responsibility; avoid coupling internal components from different features.

## Elevation & Depth

Build hierarchy with spacing, surfaces, and dividers. Avoid gradients and repeated
row shadows. Reserve `--app-dialog-shadow` (`0 24px 80px #24324726`) for dialogs
and `--app-selected-shadow` (`0 1px 3px #2432470d`) for selection when needed.
The error notice and reconnection UI have dedicated shared tokens for their
shadows and backgrounds. Do not define local shadows in components.

## Shapes

MudBlazor controls use a default border radius of **10 px**. The complementary
scale includes 8 px controls, 6 px status indicators, 16 px lists, 18 px dialogs,
and 22 px empty-state icons. Select the radius by purpose and reuse
`--app-radius-*`; do not invent radii for individual screens.

## Components

Reuse MudBlazor for buttons, outlined fields, selectors, and dialogs. Prioritize
one primary action; distinguish destructive actions using the error role.
In dialogs, separate actions from content, with cancel before confirm.
Component tokens describe their visual variants; use shared tokens when
implementing a component that does not yet exist in the base application.

Define loading, empty, error, selected, and disabled states when the flow requires
them. Preserve interaction and disabled-state cues, visible focus, and accessible
names for icon-only actions. Maintain focus order and restore focus when closing
a dialog. Respect `prefers-reduced-motion`.

`Components/App.razor` loads MudBlazor, `theme.css`, `app.css`, and isolated styles,
in that order. Tokens are declared in `:root` so they also reach popovers and
dialogs outside the content area. Keep only necessary global overrides and
comment on their purpose; do not add local families, palettes, radii, or shadows.

## Do's and Don'ts

- Reuse semantic roles and shared tokens. Add new tokens by purpose, in English,
  with the `--app-` prefix, once in `theme.css`.
- Keep the primary action clear and messages specific.
- Check WCAG AA contrast: 4.5:1 for normal text, 3:1 for large text and essential
  visual control cues. Also review hover, focus, and error states.
- Check keyboard interaction, long text, and mobile layouts after changing the theme.
- Do not rely on color alone to communicate states.
- Do not add remote fonts, decorative gradients, or automatic animations.
- Do not duplicate theme values in composition stylesheets. `transparent` and
  `currentColor` may be used as structural values.
- Do not turn this guide into a feature inventory or decision history: those
  details belong in their specs.
