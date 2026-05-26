# Shared Agent Instructions

Status: Approved

These instructions apply to Copilot, Codex, Claude Code, and any other coding agent working in this repository. Use this file as the shared operating contract. Tool-specific behavior is allowed only when it does not conflict with the repository development process.

## Source Of Truth

Before planning or changing code, read:

* [EditorConfig](../../.editorconfig)
* [Repository Navigation](repository-navigation.md)
* [Coding Standards](coding-standards.md)
* Process skills under [skills](skills/)
* [AI-Native Development Process](../dev-process/README.md)
* [Issue -> Change Request](../dev-process/issue-to-change-request.md)
* [Intent -> Change Request or ADR](../dev-process/intent-to-change-request-or-adr.md)
* [Approved ADR -> ADR Task](../dev-process/adr-to-adr-task.md)
* [Approved ADR or CR Task -> Code -> Test -> PR](../dev-process/task-to-code-test-pr.md)
* [PR Review -> Merge](../dev-process/pr-review-to-merge.md)
* [Process Skills How-To](../dev-process/skills-how-to.md)

If these instructions conflict with a more specific approved ADR, follow the ADR and mention the conflict in the task or PR notes.

## Read Order

Use this order for normal work:

1. [EditorConfig](../../.editorconfig)
2. [Repository Navigation](repository-navigation.md)
3. [Coding Standards](coding-standards.md)
4. [AI-Native Development Process](../dev-process/README.md)
5. The specific flow document for the current work.
6. The approved CR, ADR, CR Task, or ADR Task that authorizes the work.
7. Nearby source code, tests, and docs.

Do not read or act on files under `/docs/drafts/` unless the user explicitly references them.

## Core Rule

Do not turn raw input directly into implementation work.

* `Issue` becomes `Change Request`.
* `Intent` becomes `Change Request` or `ADR`.
* `CR Task` is created only after the Change Request is approved.
* `ADR Task` is created only after the ADR is approved.
* Code changes start only from an approved and linked `CR Task` or `ADR Task`.

If the human explicitly approves a task in a prompt, update that task to `Approved` and proceed to the next process step. If the human explicitly asks for an emergency or exploratory change without a prior approved artifact, state the exception in the final notes or PR description and recommend creating the missing artifact afterward.

## Agent Role

Agents may:

* Analyze issues, intents, ADRs, tasks, code, tests, and PR diffs.
* Draft Change Requests, ADRs, CR Tasks, ADR Tasks, tests, PR descriptions, and review notes.
* Implement scoped code or documentation changes from approved tasks.
* Suggest acceptance criteria, test plans, risks, and open questions.
* Keep documentation synchronized when behavior, architecture, or process changes.

Agents must not:

* Broaden scope without explicit human approval.
* Hide assumptions inside code.
* Treat generated text as approved just because an agent wrote it.
* Merge, close, or approve work unless the human owner explicitly requests it.
* Revert unrelated user changes in the working tree.

## Human Role

Humans own:

* Priority and business value.
* Approval of Change Requests and ADRs.
* Scope decisions when requirements are ambiguous.
* Final PR approval and merge timing.
* Risk acceptance when tests are skipped or checks are bypassed.

## Routing Rules

Use this routing table for new work:

| Input | First Artifact | Task Artifact |
| --- | --- | --- |
| Bug report, support case, observed defect | Change Request | CR Task after CR approval |
| Feature request or product goal | Change Request | CR Task after CR approval |
| Short product intent | Change Request or ADR | CR Task or ADR Task after approval |
| Architecture, boundary, storage, security, integration, or platform decision | ADR | ADR Task after ADR approval |
| Approved Change Request | CR Task | Code/Test/PR |
| Approved ADR | ADR Task | Code/Test/PR |

When unsure whether intent needs an ADR, prefer asking the human owner or drafting the ambiguity as an open question in the Change Request.

## Artifact Metadata

Every durable work artifact should include metadata near the top:

```yaml
id: CR-YYYYMMDD-short-slug
type: change-request
status: Draft
owner: "@github-user-or-team"
authors:
  humans:
    - "@github-user"
  agents:
    - "Copilot | Codex | Claude Code"
source:
  type: issue | intent | adr | change-request
  ref: "#123 or relative/path.md"
agent_context:
  agent: "Copilot | Codex | Claude Code"
  model: "model name when known"
  tools:
    - "tool name when relevant"
created: YYYY-MM-DD
updated: YYYY-MM-DD
```

Use the actual agent name in `authors.agents`. If multiple agents contributed, list each one. Record model and tool names in `agent_context` when known.

## Status Rules

Use the repository status vocabulary:

| Status | Use |
| --- | --- |
| `Draft` | Artifact is being shaped. |
| `Ready` | Artifact is ready for owner approval review. |
| `Changes Requested` | Owner or reviewer requires updates. |
| `Approved` | Owner accepts the artifact or PR. |
| `In Progress` | Implementation work has started from an approved task. |
| `Review` | PR or formal review is active. |
| `Merged` | PR has been merged. |
| `Closed` | No further work is planned. |

Move from `Ready` to `Approved` only when the human owner approves. Agents may update an artifact to `Approved` only when the human owner explicitly approves it in the prompt. `Draft` artifacts do not move to the next process step.

## Naming Rules

Follow the naming convention from the process README:

| Artifact | Pattern |
| --- | --- |
| ADR | `ADR-YYYYMMDD-short-slug.md` |
| Change Request | `CR-YYYYMMDD-short-slug.md` |
| CR Task | `CR-TASK-YYYYMMDD-short-slug.md` |
| ADR Task | `ADR-TASK-YYYYMMDD-short-slug.md` |
| Branch | `<agent-name>/<artifact-id>-short-slug` or the human-requested branch name |
| PR title | `<artifact-id>: short summary` |

Use lowercase slugs with hyphens. Keep file names stable after review starts.

## Before Editing

Before changing files:

* Identify the source task, confirm it links to an approved CR or ADR, and confirm the task itself is `Approved`.
* Respect [EditorConfig](../../.editorconfig): UTF-8, CRLF, final newline, and no trailing whitespace.
* Read [Repository Navigation](repository-navigation.md) and [Coding Standards](coding-standards.md).
* Read nearby code, tests, and docs.
* Check the working tree for unrelated changes.
* Keep the change scoped to the approved task.
* If the task is missing or not approved, draft or update the required CR/ADR/task instead of jumping to code.

## Implementation Rules

When implementing:

* Preserve existing project patterns unless the task or ADR says otherwise.
* Prefer small, reviewable changes.
* Update docs when behavior, public API, architecture, configuration, or process changes.
* Record assumptions and follow-up work in the task or PR notes.
* Do not silently introduce new dependencies, services, or architecture boundaries.
* Do not include deployment or artifact publishing steps until that process is documented.

## Test Rules

Choose verification based on risk:

| Change Type | Expected Verification |
| --- | --- |
| Documentation only | Markdown/link/path check when practical |
| Small isolated behavior | Unit test |
| API behavior | Unit test plus request-level or integration test when available |
| Data access or integration boundary | Integration, contract, or documented manual verification |
| Architecture change | Focused tests plus ADR consequence review |

Before opening or updating a PR, run all repository tests unless the human owner explicitly accepts the exception. If tests are skipped, state why and name the residual risk.

## PR Rules

Every PR should include:

* Link to the source `CR Task` or `ADR Task`.
* Link from that task back to the approved `Change Request` or `ADR`.
* Summary of changes.
* Verification performed.
* Tests skipped and rationale, if any.
* Known risks, assumptions, or follow-up tasks.

Review the PR against [PR Review -> Merge](../dev-process/pr-review-to-merge.md).

## Open Questions

If process guidance is missing or ambiguous:

* Add task-specific questions to the current CR, ADR, or task artifact.
* Ask the human owner only when the answer blocks safe progress.

## Markdown Rules

Use Markdown that works well in VS Code, GitHub, and Mermaid preview:

* Use one `#` heading per file.
* Use short sections and stable headings.
* Use fenced code blocks with language tags.
* Use relative links inside the repository.
* Use Mermaid diagrams for process flow when helpful.
* Write process artifacts in English, regardless of the prompt language.
* Save Markdown as UTF-8 with CRLF line endings, following [EditorConfig](../../.editorconfig).

