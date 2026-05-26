---
name: arenal-cr-to-task
description: Create an Arenal CR Task from an approved Change Request. Use when the user asks to turn an approved CR into implementation work, create docs/tasks/CR-TASK-YYYYMMDD-short-slug.md, or prepare scoped task artifacts before coding.
---

# Arenal Change Request To Task

## Workflow

1. Read `docs/agents/README.md`, `docs/dev-process/README.md`, `docs/dev-process/issue-to-change-request.md`, and `docs/dev-process/task-to-code-test-pr.md`.
2. Read the referenced Change Request from `docs/change-requests/`.
3. Confirm the CR has `Status: Approved`, or that the human explicitly approves it in the prompt.
4. Create one or more `CR-TASK-YYYYMMDD-short-slug.md` files under `docs/tasks/`.
5. Use the CR Task skeleton from `docs/dev-process/task-to-code-test-pr.md`.
6. Set each new task to `Status: Draft` or `Status: Ready`; set `Status: Approved` only if the human explicitly approves the task.
7. Link each task back to the approved CR and keep implementation scope inside the approved CR.

## Guardrails

- Do not broaden scope while translating a CR into tasks.
- Include acceptance criteria, impacted areas, required changes, verification plan, risks, assumptions, and open questions.
- Prefer multiple small tasks over one broad task when the CR spans unrelated areas.
- Do not edit code from this skill unless the task is already `Approved` and the user asks to proceed.
