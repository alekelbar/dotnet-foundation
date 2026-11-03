---
name: sync-design-system
description: Synchronize docs/DESIGN.md with the project's MudBlazor theme and shared styles. Use when asked to apply design guide changes or resolve differences between its tokens and the implementation, rather than for general UI changes.
---

# Synchronize the design system

Synchronize in one direction: `docs/DESIGN.md` → visual configuration. Keep the
document as the design source compatible with Stitch's DESIGN.md format, without
generating code during the build or adding dependencies. For review-only requests,
report differences without editing files.

## Read and compare

- Work from the repository root. Read `AGENTS.md`, `docs/architecture.md`,
  and `docs/DESIGN.md`. This skill does not authorize modifying `AGENTS.md` or
  `docs/`: editing those files requires an explicit request directed at them.
- Locate the Presentation project by finding `Components/Theming/AppTheme.cs`,
  `wwwroot/theme.css`, and `wwwroot/app.css` under `src/`. Its initial name is
  `AppTemplate.Presentation`, but it may change when reusing the template.
  Also read the layout and `Components/App.razor` to check how the theme is
  applied and styles are loaded. Do not assume absolute paths from this machine.
- Compare the full current values in the guide and code. Use Git to identify
  others' changes and review your diff, rather than as the sole source for
  detecting differences: a divergence may already have been committed.
- Read YAML tokens from both front matter and YAML blocks in the body, when
  present. Resolve references such as `{colors.primary}`; detect missing
  references, cycles, or duplicate top-level keys before applying them.
  Tokens determine values; prose explains their purpose and application.
  If they contradict each other in a way that affects the decision, report the
  conflict and leave that part pending; continue with clear, independent changes.

## Apply changes at the responsible source

| Destination relative to Presentation | Responsibility |
| --- | --- |
| `Components/Theming/AppTheme.cs` | MudBlazor roles, shared typography, and default border radius. |
| `wwwroot/theme.css` | Aliases to MudBlazor variables and complementary tokens in `:root`. |
| `wwwroot/app.css` | Shared rules that consume tokens; change composition only when the guide requires it. |

- Map by meaning, rather than similar names or equal values. In the current
  guide, `colors.secondary` represents secondary text: it maps to `TextSecondary`,
  not automatically to `PaletteLight.Secondary`. `surface` and `on-surface` also
  supply the app bar and text roles that use those same surfaces. Do not reassign
  a token to another role merely because they share a value.
- Preserve aliases such as `--app-accent: var(--mud-palette-primary)`: change the
  C# role instead of copying its hex value into CSS. Include foreground text
  colors for roles when the guide defines them, and check that no remaining
  MudBlazor defaults contradict those tokens.
- Preserve the creation of a fresh theme per layout; do not share a mutable
  `MudTheme` across circuits. Keep the light-mode policy while the guide requires it.
- Synchronize complementary colors, sizes, weights, line heights, spacing,
  radii, and shadows according to their actual uses. Respect rules expressed
  in prose: currently, the YAML title size is the maximum of a `clamp()`,
  rather than a fixed size for every viewport width.
- Reuse existing tokens. Add a shared token named for its purpose, in English
  and prefixed with `--app-`, only when an explicit rule requires a new one.
  Do not introduce local palettes, fonts, radii, or shadows, or remote fonts.
- An omitted token does not instruct you to delete its implementation or reset
  it to defaults. Preserve unspecified values, including technical error and
  reconnection configuration. If a new value has no clear mapping or requires
  redesigning a screen, report it as pending without inventing a mapping.
  Component tokens guide existing components; they do not create features.
- Limit changes to these shared configuration points. You may inspect consumers
  to check impact; if features or isolated styles need changes, identify them
  and verify that the request covers them before expanding the scope.
  If values already match, do not make cosmetic changes or rewrite files.

## Verify and deliver

- Review the diff and CSS references for modified tokens: no broken aliases,
  duplication, or changes to others' work. Run `git diff --check`.
- If you change C#, build the solution or affected project using the commands
  available in the repository. For CSS, check loading and computed styles in
  the application when possible; building does not validate appearance.
- According to the impact, review a sample of affected surfaces and controls,
  including desktop and 320 px mobile layouts, long text, keyboard focus,
  and hover, error, and disabled states. For color changes, check AA contrast;
  preserve reduced-motion behavior. There is no need to review unrelated screens.
- Summarize synchronized values and files, conflicts or unmapped items, and
  checks performed. If you could not build or open the UI, state that without
  presenting those checks as passed.
