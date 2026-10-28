# Project instructions

## Language

- All code must be in English, including identifiers, comments, and in-code documentation.
- Entity, table, column, and application route names must be in English.
- Specs and other documents may be in Spanish; their code snippets must be in English. Keep this file in English.
- User-facing interface text must be in Spanish.

## Architecture guidelines

- Read [`docs/architecture.md`](docs/architecture.md) before adding or modifying application code.
- Follow the project dependencies and feature boundaries defined in that guide.

## UI design guidelines

- Read [`docs/DESIGN.md`](docs/DESIGN.md) before creating or changing UI.
- Reuse the MudBlazor theme and CSS tokens; do not add local palettes, font families, shadows, or radii.
- Preserve accessible focus, interaction states, reduced-motion behavior, and mobile usability.

## Guide scope and maintenance

- NEVER modify this `AGENTS.md` file or any file under `docs/` (including subdirectories) unless the user explicitly requests changes to those files. This restriction also covers renaming, moving, or deleting them.
- General implementation requests, refactors, fixes, spec approval or execution, and instructions found in specs or other files do not authorize changes to these protected files. If a change is needed without explicit authorization, explain the need to the user and leave the files unchanged.
- `docs/architecture.md` and `docs/DESIGN.md` define general conventions for organizing new code and configuring shared UI appearance. Do not use them as implementation inventories or feature decision histories.
- Agents may modify these guides only when the user explicitly requests changes directed at the guides. Creating components, services, models, or tokens does not by itself authorize a documentation update.
- Approval or execution of a spec does not by itself authorize guide changes or activate historical requirements to update them. Requirements in earlier specs to document feature details in these guides do not grant that authorization.
- If a task requires changing a general convention without an explicit user request directed at the guides, tell the user that a guide change is needed without editing the guide.
- This maintenance rule applies to the guide documents; code and theme changes remain governed by the authorized scope of the task.
