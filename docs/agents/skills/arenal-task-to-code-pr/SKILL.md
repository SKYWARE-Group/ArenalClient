---
name: arenal-task-to-code-pr
description: Implement an approved Arenal CR Task or ADR Task and prepare pull request notes. Use when the user asks to code from docs/tasks/CR-TASK-...md or docs/tasks/ADR-TASK-...md, run verification, update docs/tests, or prepare a PR from an approved task.
---

# Arenal Task To Code And PR

## Workflow

1. Read `docs/agents/README.md`, `docs/agents/repository-navigation.md`, `docs/agents/coding-standards.md`, and `docs/dev-process/task-to-code-test-pr.md`.
2. Read the source task under `docs/tasks/`.
3. Confirm the task has `Status: Approved`, or that the human explicitly approves it in the prompt and asks to proceed.
4. Confirm the task links to an approved CR or ADR.
5. Inspect nearby source, tests, and docs before editing.
6. Keep changes scoped to the task.
7. Add or update focused tests when behavior changes.
8. Run focused verification while developing, then run all repository tests before PR unless the human explicitly waives that requirement.
9. Prepare PR notes using `.github/pull_request_template.md`.

## Guardrails

- Preserve unrelated working tree changes.
- Do not introduce dependencies, services, architecture boundaries, or deployment steps outside the approved task.
- Use branch naming `<agent-name>/<task-id>-short-slug` when creating a branch.
- Record skipped tests and residual risk when an owner-approved exception exists.
- Update docs when behavior, public API, architecture, configuration, process, or repository structure changes.
