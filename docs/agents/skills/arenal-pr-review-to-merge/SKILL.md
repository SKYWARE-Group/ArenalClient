---
name: arenal-pr-review-to-merge
description: Review an Arenal pull request against its source CR Task or ADR Task, acceptance criteria, verification, scope, docs, and repository process. Use when the user asks for PR review, merge readiness, review notes, missing tests, or process compliance before merge.
---

# Arenal PR Review To Merge

## Workflow

1. Read `docs/agents/README.md`, `docs/dev-process/pr-review-to-merge.md`, and the PR source task under `docs/tasks/`.
2. Confirm the PR links to a source CR Task or ADR Task.
3. Confirm the source task links back to an approved CR or ADR.
4. Review the diff against task scope and acceptance criteria.
5. Check that all repository tests passed, or that the owner documented an exception.
6. Check docs, security, compatibility, and data-impact concerns when relevant.
7. Report findings first, ordered by severity, with file and line references when reviewing code.

## Guardrails

- Do not approve, merge, close, or mark work merged unless the human owner explicitly asks.
- Treat unrelated diff as a review finding.
- Prefer concrete findings over summaries.
- If there are no findings, state that clearly and mention remaining test gaps or residual risk.
