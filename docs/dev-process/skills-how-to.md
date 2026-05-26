# Process Skills How-To

Status: Approved

This repository has Codex skills for the main AI-native development process steps. Use them when you want an agent to follow one process transition instead of improvising from a broad prompt.

The canonical skill definitions live in `docs/agents/skills/`. Keep that repository copy as the source of truth so colleagues and other agents can inspect, reuse, or install the same workflows.

## Available Skills

| Skill | Use for |
| --- | --- |
| `arenal-issue-to-cr` | Turn a concrete issue, bug report, support case, or observed defect into a Change Request. |
| `arenal-intent-to-cr-or-adr` | Turn a short intent into either a Change Request or an ADR. |
| `arenal-adr-to-task` | Turn an approved ADR into one or more ADR Tasks. |
| `arenal-cr-to-task` | Turn an approved Change Request into one or more CR Tasks. |
| `arenal-task-to-code-pr` | Implement an approved CR Task or ADR Task and prepare PR notes. |
| `arenal-pr-review-to-merge` | Review a PR against its source task, acceptance criteria, tests, and process rules. |

## Codex Setup

Some Codex environments auto-discover only user-level skills. If these repo-local skills are not listed in the session, copy or sync the folders from `docs/agents/skills/` into your Codex skills directory, such as `%USERPROFILE%\.codex\skills` on Windows.

```powershell
Copy-Item .\docs\agents\skills\arenal-* $env:USERPROFILE\.codex\skills\ -Recurse -Force
```

After that, start a new Codex session so the skills appear in the available skills list.

## Simple Examples

Create a Change Request from an issue:

```text
Use arenal-issue-to-cr for this bug report and create the CR in docs/change-requests.
```

Route a product idea:

```text
Use arenal-intent-to-cr-or-adr for this intent. Decide whether it should become a CR or ADR.
```

Create an ADR task:

```text
Use arenal-adr-to-task for docs/adr/ADR-YYYYMMDD-short-slug.md.
```

Create a CR task:

```text
Use arenal-cr-to-task for docs/change-requests/CR-YYYYMMDD-short-slug.md.
```

Implement an approved task:

```text
Use arenal-task-to-code-pr for docs/tasks/CR-TASK-YYYYMMDD-short-slug.md.
```

Review a PR:

```text
Use arenal-pr-review-to-merge and review this PR against its source task.
```

## Approval Reminder

Agents may draft artifacts, but the next process step starts only after human approval. If you want an agent to approve and continue in the same prompt, say so explicitly:

```text
I approve this task. Mark it Approved and proceed with implementation.
```

Process artifacts should be written in English, even when the prompt is in another language.
