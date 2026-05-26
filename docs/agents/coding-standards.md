# Coding Standards

Status: Approved

Follow existing conventions in the relevant project first. These standards exist to keep Copilot, Codex, Claude Code, and human contributors aligned while the repository evolves.

## General Rules

* Keep diffs small and focused.
* Match nearby naming, namespaces, file organization, and formatting.
* Prefer consistency with local code over generic style assumptions.
* Do not rename, move, or re-layer files unless the approved task requires it.
* Do not introduce dependencies without clear need and human approval.
* Do not refactor unrelated code while implementing a task.
* Preserve unrelated working tree changes.
* Respect [EditorConfig](../../.editorconfig) for encoding, line endings, final newline, and trailing whitespace.

## Encoding And Line Endings

The repository uses a root [EditorConfig](../../.editorconfig) file as the source of truth for basic file formatting.

* Use UTF-8.
* Use CRLF line endings.
* Insert a final newline.
* Trim trailing whitespace.
* Do not create editor-specific exceptions unless the approved task requires them.

## Repository Structure Rules

Repository structure is part of the design.

* Read [Repository Navigation](repository-navigation.md) before structural changes.
* Treat `/src/Arenal.ApiModel/` as the core public data model package.
* Treat `/src/Arenal.WebClient/` as the reusable HTTP client package.
* Treat `/src/Arenal.Forms.Bg/` as the Bulgaria-specific forms package.
* Treat `/src/Arenal.ApiModel.Test/` as the main unit test project.
* Treat `/src/Arenal.WebClient.IntegrationTest/` as the integration test project.
* Prefer existing folders over new folders.
* Keep current folder names and spelling unless a task explicitly changes them.
* Do not create parallel structures at repository root.

## .NET And C# Rules

This repository contains `netstandard2.0` package projects and `net10.0` test/tooling projects.

* Keep library code compatible with each project's declared `TargetFramework`.
* Follow current .NET and C# naming conventions where compatible with the target framework.
* Use the existing namespace style in the target folder.
* Use one primary type per file unless nearby code already groups small related types.
* Prefer `nameof()` over hard-coded member names.
* Prefer `obj is null` and `obj is not null` where it improves clarity and remains compatible.
* Prefer `string.Empty` for intentional empty strings.
* Use explicit types for primitive and simple values when that improves readability.
* Use `var` when the type is obvious from the right-hand side.
* Avoid broad catch blocks unless existing behavior or boundary handling requires them.
* Add comments only when they explain non-obvious intent, constraints, or risk.

## Public Package Contract Rules

The model and client projects are reusable NuGet-style packages.

* Treat public model types, property names, enum values, validators, and client extension methods as package contracts.
* Do not rename or remove public members without an approved CR or ADR that calls out compatibility impact.
* Preserve JSON serialization expectations and wire-level names unless an approved task changes them.
* Keep XML documentation current when public package behavior or members change.
* Keep package README files under `Assets/` aligned with externally visible behavior.
* Do not change package metadata, versions, license, icons, or package IDs unless the task explicitly requires release metadata work.

## Arenal.ApiModel Rules

The core model package owns public contracts, validation, filters, localization, tracking, and discovery helpers.

* Put public Arenal domain entities under `/src/Arenal.ApiModel/Model/`.
* Put query/filter behavior under `/src/Arenal.ApiModel/Filters/`.
* Put FluentValidation validators under `/src/Arenal.ApiModel/Validation/`.
* Keep validation error behavior stable and covered by tests when changed.
* Keep localization resources under `/src/Arenal.ApiModel/L10n/`.
* Update `.resx` files as the source of truth for resources; avoid hand-editing generated designer files unless regeneration is not available and the change is reviewed.
* Preserve existing Bulgarian localization where present.

## Arenal.WebClient Rules

The web client package is a reusable HTTP client surface for Arenal.

* Keep client APIs small, predictable, and aligned with existing extension patterns.
* Avoid adding credentials, endpoint URLs, or environment-specific settings to source.
* Do not introduce live service assumptions into package code.
* Add integration tests only for behavior that cannot be verified by local tests.
* Document authentication, endpoint, or request/response behavior changes in the DocFX docs or package README when user-facing.

## Documentation Rules

When behavior, contracts, process, or repository structure changes, update relevant documentation.

* Product and package documentation belongs under `/src/Arenal.ApiModel.Documentation/` or the relevant package `Assets/` README.
* Agent operating rules belong under `/docs/agents/`.
* AI-native process rules belong under `/docs/dev-process/`.
* Use relative links inside Markdown documentation.
* Use Mermaid diagrams for process flow when helpful.
* Follow [EditorConfig](../../.editorconfig): UTF-8, CRLF, final newline, and no trailing whitespace.

## Security Rules

Security-sensitive changes require explicit scope.

* Do not add secrets, credentials, tokens, live URLs, or local user-specific paths to source files or documentation.
* Treat integration test configuration and user secrets as sensitive.
* Do not relax authentication or authorization assumptions in client flows casually.
* If a task reveals a hard-coded secret or risky configuration, report it in notes and avoid broad cleanup unless asked.

## Test Rules

The test projects use NUnit.

* Add fast, isolated tests under `/src/Arenal.ApiModel.Test/` for model, filter, tracking, and validation behavior.
* Use `[Test]`, `[TestCase]`, and existing NUnit patterns consistently with nearby tests.
* Add integration tests under `/src/Arenal.WebClient.IntegrationTest/` only when a configured Arenal service boundary is necessary.
* Keep integration test requirements explicit; do not hide required credentials or service state.
* Prefer regression tests for bug fixes.
* Run the smallest useful `dotnet test` command first.
* If a test cannot be run locally, state why and document residual risk.

Useful commands:

```powershell
dotnet test .\src\Arenal.ApiModel.Test\Arenal.ApiModel.Test.csproj
dotnet test .\src\Arenal.WebClient.IntegrationTest\Arenal.WebClient.IntegrationTest.csproj
dotnet test .\ArenalClient.slnx
```

## Change Request And ADR Rules

Follow the repository process before implementation:

* Raw Issue -> Change Request.
* Raw Intent -> Change Request or ADR.
* Approved Change Request -> CR Task.
* Approved ADR -> ADR Task.
* Approved CR Task or ADR Task -> Code/Test/PR.

Read:

* [Issue -> Change Request](../dev-process/issue-to-change-request.md)
* [Intent -> Change Request or ADR](../dev-process/intent-to-change-request-or-adr.md)
* [Approved ADR -> ADR Task](../dev-process/adr-to-adr-task.md)
* [Approved ADR or CR Task -> Code -> Test -> PR](../dev-process/task-to-code-test-pr.md)

Do not implement code merely because a Change Request or ADR exists. Implementation starts from the approved linked task. If the human owner explicitly approves a task in a prompt, update that task to `Approved` before proceeding.

## Before Generating Code

Before generating or editing code:

1. Confirm the work is based on an approved `CR Task` or `ADR Task`, unless the human explicitly requests an exception.
2. Inspect the repository tree and owning project.
3. Read nearby files in the same folder.
4. Match local namespace and dependency conventions.
5. Check whether similar code already exists.
6. Identify the matching test area.
7. Keep the change scoped to the approved task.

## After Changing Code

After changing code:

1. Run all repository tests before PR unless the human owner explicitly accepts an exception.
2. Update tests when behavior changed.
3. Update docs when contracts, process, or structure changed.
4. Summarize changed files and verification.
5. Record assumptions, skipped tests, and follow-up work in task or PR notes.
