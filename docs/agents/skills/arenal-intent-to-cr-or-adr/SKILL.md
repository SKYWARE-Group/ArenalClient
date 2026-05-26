---
name: arenal-intent-to-cr-or-adr
description: Route a short Arenal product, behavior, documentation, implementation, architecture, storage, security, integration, or platform intent into either a Change Request or an ADR. Use when the user gives raw intent and wants the correct approval artifact before tasks or code.
---

# Arenal Intent To CR Or ADR

## Workflow

1. Read `docs/agents/README.md`, `docs/dev-process/README.md`, and `docs/dev-process/intent-to-change-request-or-adr.md`.
2. Decide whether the intent is product/behavior scoped or requires an architectural decision.
3. Create a Change Request under `docs/change-requests/` for product, behavior, documentation, or implementation-scoped change.
4. Create an ADR under `docs/adr/` for architecture, boundary, storage, security, integration, or platform decisions.
5. Use `CR-YYYYMMDD-short-slug.md` or `ADR-YYYYMMDD-short-slug.md`.
6. Set `Status: Draft` unless the human explicitly approves the artifact in the prompt.
7. Do not create implementation tasks or code unless the artifact is `Approved` or the human explicitly approves the next step.

## Guardrails

- Prefer a narrow first CR when the intent is broad.
- Route architectural decisions to ADR instead of hiding them in a CR.
- Separate confirmed facts from assumptions.
- Ask only blocking questions; otherwise record open questions in the artifact.
- Write process artifacts in English.
