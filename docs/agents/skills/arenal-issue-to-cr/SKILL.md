---
name: arenal-issue-to-cr
description: Draft an Arenal Change Request from a concrete issue, bug report, support case, observed defect, customer request, or production behavior. Use when the user asks to turn an issue into a CR, normalize a bug into process artifacts, or prepare docs/change-requests/CR-YYYYMMDD-short-slug.md before implementation.
---

# Arenal Issue To Change Request

## Workflow

1. Read `docs/agents/README.md`, `docs/dev-process/README.md`, and `docs/dev-process/issue-to-change-request.md`.
2. Inspect nearby repository context only enough to identify affected areas and test expectations.
3. Create or update one English Change Request under `docs/change-requests/`.
4. Use `CR-YYYYMMDD-short-slug.md` with a stable lowercase slug.
5. Set `Status: Draft` unless the human explicitly approves the CR in the prompt.
6. Include source issue, owner when known, authors, agent context, problem, scope, non-scope, acceptance criteria, implementation notes, test expectations, and open questions.
7. Do not create a CR Task or edit code unless the CR is `Approved` or the human explicitly approves the next step.

## Guardrails

- Preserve the original issue context; do not invent business rules.
- Make acceptance criteria testable.
- Put unresolved task-specific questions in the CR.
- Write process artifacts in English, regardless of the prompt language.
- Keep `docs/drafts/` out of scope unless the user explicitly references it.
