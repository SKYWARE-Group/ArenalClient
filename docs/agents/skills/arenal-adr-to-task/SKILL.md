---
name: arenal-adr-to-task
description: Create an Arenal ADR Task from an approved Architecture Decision Record. Use when the user asks to turn an approved ADR into implementation work, create docs/tasks/ADR-TASK-YYYYMMDD-short-slug.md, or derive scoped code/documentation/test work from an ADR.
---

# Arenal ADR To Task

## Workflow

1. Read `docs/agents/README.md`, `docs/dev-process/README.md`, and `docs/dev-process/adr-to-adr-task.md`.
2. Read the referenced ADR from `docs/adr/`.
3. Confirm the ADR has `Status: Approved`, or that the human explicitly approves it in the prompt.
4. Create one or more `ADR-TASK-YYYYMMDD-short-slug.md` files under `docs/tasks/`.
5. Use the ADR Task skeleton from `docs/dev-process/adr-to-adr-task.md`.
6. Set each new task to `Status: Draft` or `Status: Ready`; set `Status: Approved` only if the human explicitly approves the task.
7. Link each task back to the ADR and keep implementation scope inside the approved decision.

## Guardrails

- Do not reopen or redesign the ADR while creating the task.
- Split large ADRs into multiple focused tasks when useful.
- Include impacted areas, required changes, verification plan, compatibility and migration notes, risks, assumptions, and open questions.
- Do not edit code from this skill unless the task is already `Approved` and the user asks to proceed.
