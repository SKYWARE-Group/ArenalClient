# Repository Navigation

Status: Approved

This repository may not follow the default layout expected by IDE assistants. Always verify actual paths before creating files, changing namespaces, or adding project references.

## Important Structure Rule

Do not assume the repository root is the code root.

In this repository:

* The solution file is at `/ArenalClient.slnx`.
* Reusable packages and tools live under `/src/`.
* Unit tests live under `/src/Arenal.ApiModel.Test/`.
* Integration tests live under `/src/Arenal.WebClient.IntegrationTest/`.
* DocFX documentation lives under `/src/Arenal.ApiModel.Documentation/`.
* Agent instructions live under `/docs/agents/`.
* Shared process skills live under `/docs/agents/skills/`.
* AI-native development process documentation lives under `/docs/dev-process/`.
* ADRs live under `/docs/adr/`.
* Change Requests live under `/docs/change-requests/`.
* Tasks live under `/docs/tasks/`.

## Navigation Workflow

Before making any structural or code decision:

1. Inspect the repository tree.
2. Locate `/ArenalClient.slnx`.
3. Locate the owning project file.
4. Inspect the target folder and nearby files.
5. Read relevant agent and process docs.
6. Only then generate or edit code.

## Source Priority

When multiple sources exist, use this priority order:

1. Agent docs under `/docs/agents/`.
2. Approved process artifacts under the paths defined by [AI-Native Development Process](../dev-process/README.md).
3. Approved ADRs and Change Requests when they exist.
4. Existing code in the relevant project.
5. Tests and examples.
6. Package readmes and DocFX documentation.
7. Raw issue, PR, support, or prompt discussion.

## Project Map

| Path | Purpose |
| --- | --- |
| `/.editorconfig` | Repository-wide encoding and line-ending rules. |
| `/ArenalClient.slnx` | Main solution file. |
| `/src/Arenal.ApiModel/Arenal.ApiModel.csproj` | Core Arenal API data model package. |
| `/src/Arenal.ApiModel/Model/` | Public model contracts. Treat public members as package/API contracts. |
| `/src/Arenal.ApiModel/Filters/` | AMQL/filter model and helpers. |
| `/src/Arenal.ApiModel/Validation/` | FluentValidation validators for model contracts. |
| `/src/Arenal.ApiModel/L10n/` | RESX localization resources and generated designer files. |
| `/src/Arenal.WebClient/Arenal.WebClient.csproj` | Reusable Arenal Web API client package. |
| `/src/Arenal.WebClient/HttpClientExtensions.cs` | Client extension surface. |
| `/src/Arenal.Forms.Bg/Arenal.Forms.Bg.csproj` | Bulgaria-specific forms data model package. |
| `/src/Arenal.WebClient.Demo/Arenal.WebClient.Demo.csproj` | Console/demo project for exercising the web client. |
| `/src/Arenal.ApiModel.CliApp/Arenal.ApiModel.CliApp.csproj` | CLI helper project. |
| `/src/Arenal.ApiModel.Test/Arenal.ApiModel.Test.csproj` | NUnit unit tests for the data model and validators. |
| `/src/Arenal.WebClient.IntegrationTest/Arenal.WebClient.IntegrationTest.csproj` | NUnit integration tests for web client scenarios. |
| `/src/Arenal.ApiModel.Documentation/` | DocFX documentation project. |
| `/docs/dev-process/` | Process documentation and Mermaid flows. |
| `/docs/agents/` | Shared instructions for Copilot, Codex, Claude Code, and other agents. |
| `/docs/agents/skills/` | Repository-owned process skills for agents and humans. |
| `/docs/adr/` | Architecture Decision Records. |
| `/docs/change-requests/` | Change Request artifacts. |
| `/docs/tasks/` | CR Task and ADR Task artifacts. |

## Placement Guidance

* Solution-level changes should update `/ArenalClient.slnx` only when a project is intentionally added or removed.
* Public contract changes normally belong in `/src/Arenal.ApiModel/Model/`, validators in `/src/Arenal.ApiModel/Validation/`, and filter/query behavior in `/src/Arenal.ApiModel/Filters/`.
* Client behavior changes belong under `/src/Arenal.WebClient/` and should be tested through unit tests where possible or integration tests when the behavior crosses the HTTP boundary.
* Bulgaria-specific form model changes belong under `/src/Arenal.Forms.Bg/`.
* Tests should be added to `/src/Arenal.ApiModel.Test/` for isolated model, filter, tracking, and validation behavior.
* Integration tests should be added to `/src/Arenal.WebClient.IntegrationTest/` only when they need configured credentials or a live service boundary.
* Documentation changes belong under `/src/Arenal.ApiModel.Documentation/` for product/package docs or `/docs` for process and agent docs.
* Change Requests, ADRs, and Tasks should follow the structure described in [AI-Native Development Process](../dev-process/README.md).

## Development Process Navigation

Use these links when routing work:

* [Issue -> Change Request](../dev-process/issue-to-change-request.md)
* [Intent -> Change Request or ADR](../dev-process/intent-to-change-request-or-adr.md)
* [Approved ADR -> ADR Task](../dev-process/adr-to-adr-task.md)
* [Approved ADR or CR Task -> Code -> Test -> PR](../dev-process/task-to-code-test-pr.md)
* [PR Review -> Merge](../dev-process/pr-review-to-merge.md)

Remember: do not create code from raw Issue or Intent. Create or update the approval artifact first.

## Common Failure Modes To Avoid

* Creating a second solution because `/ArenalClient.slnx` was not checked.
* Adding code at repository root when the owning project is under `/src`.
* Changing public model members without treating them as package contracts.
* Editing generated RESX designer files when the `.resx` source should be updated instead.
* Adding integration tests for behavior that can be covered by fast unit tests.
* Introducing live service, credential, or environment assumptions into unit tests.
* Creating parallel folders because a nearby convention was not inspected.
* Renaming existing folders or files to fix spelling or style without an approved task.
* Implementing directly from raw intent before CR or ADR approval.

## Practical Reminder For IDE Agents

Visual Studio, Copilot, Codex, and Claude Code may bias toward the currently opened file or project view. Verify actual paths before generating files.

Before editing, confirm:

* whether the change belongs to `/src`, `/docs`, `.github`, or another existing area
* the owning `.csproj`
* the namespace used by nearby files
* the matching unit or integration test location
* whether the work starts from an approved `CR Task` or `ADR Task`
