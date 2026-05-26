# CR-TASK-20260526-organization-api-key-model

```yaml
id: CR-TASK-20260526-organization-api-key-model
type: cr-task
status: In Progress
owner: "TBD"
authors:
  humans:
    - "Human"
  agents:
    - "Codex"
source:
  type: change-request
  ref: "../change-requests/CR-20260526-organization-api-key-model.md"
agent_context:
  agent: "Codex"
  model: "GPT-5"
  tools:
    - "shell"
    - "apply_patch"
created: 2026-05-26
updated: 2026-05-26
```

## Change Request Summary

[CR-20260526-organization-api-key-model](../change-requests/CR-20260526-organization-api-key-model.md) approves adding a public Arenal API model contract for organization-owned API key metadata. The model must let Arenal Web Server persist non-plaintext API key verification metadata inside each `Organization` document while avoiding plaintext secrets, usage tracking, and first-slice revocation metadata.

## Implementation Objective

Add the shared `Skyware.Arenal.ApiModel.Model` contract for organization API keys by introducing an `OrganizationApiKey` model type and an `Organization.ApiKeys` collection that defaults to an empty collection.

## Scope

* Add a public `OrganizationApiKey` model type under `src/Arenal.ApiModel/Model/`.
* Add `ApiKeys` to `Organization` in `src/Arenal.ApiModel/Model/Organization.cs`.
* Include key id, client id or display name, non-secret HMAC verification value, active/disabled status, creation timestamp, and optional expiration timestamp.
* Add XML documentation that makes clear the model stores metadata and non-plaintext verification material only.
* Add focused unit/contract tests under `src/Arenal.ApiModel.Test/`.
* Update package documentation only if the new public model surface is documented for consumers.

## Non-Scope

* Implementing API key authentication, generation, validation, revocation, rotation, or administration endpoints.
* Adding server-side pepper configuration.
* Storing plaintext API keys, API key secrets, or server-side pepper values in the model.
* Adding revocation-specific metadata such as `Revoked`, `RevokedAt`, or `RevokedBy`.
* Adding `lastUsed`, `lastUsedAt`, or equivalent usage tracking fields.
* Adding display/localization resources for API key metadata or `Organization.ApiKeys`.
* Renaming or removing existing public `Organization` members.
* Changing package metadata, package id, versioning, or release process.

## Acceptance Criteria

* `OrganizationApiKey` is public in the `Skyware.Arenal.ApiModel.Model` namespace.
* `Organization.ApiKeys` is public and can embed `OrganizationApiKey` records in serialized organization documents.
* `Organization.ApiKeys` defaults to an empty collection while existing serialized organization documents that omit API key data remain compatible.
* The API key model includes only non-secret verification material and administration metadata approved by the CR.
* The API key model does not include plaintext key, secret, pepper, revocation-specific metadata, `lastUsed`, or equivalent usage tracking fields.
* Existing `Organization` public members are not renamed or removed.
* XML documentation is present for the new public type and property.
* No display/localization resources are added for this first model change.
* Focused tests verify the model shape, default collection behavior, and serialized field set.
* Package documentation is updated if the implementation exposes or documents the new public surface for package consumers.

## Impacted Areas

* `src/Arenal.ApiModel/Model/Organization.cs`
* `src/Arenal.ApiModel/Model/OrganizationApiKey.cs`
* `src/Arenal.ApiModel.Test/`
* `src/Arenal.ApiModel/Assets/model-nuget-readme.md`, only if package-facing documentation is updated

## Required Changes

* Inspect nearby model classes and match existing namespace, file organization, XML documentation, and property conventions.
* Add `OrganizationApiKey` with properties for key id, client id or display name, non-secret HMAC verification value, active/disabled status, created timestamp, and optional expiration timestamp.
* Add `ApiKeys` to `Organization` with an empty collection default.
* Keep the implementation compatible with the `netstandard2.0` package target.
* Add or update NUnit tests that instantiate the public model, verify `Organization.ApiKeys` is empty by default, and assert the serialized shape excludes disallowed secret, revocation, and usage-tracking fields.
* Review package README or DocFX documentation and update only when the new public contract should be visible there.

## Verification Plan

* Run `dotnet test .\src\Arenal.ApiModel.Test\Arenal.ApiModel.Test.csproj`.
* Run `dotnet test .\ArenalClient.slnx` before PR unless the owner explicitly accepts a skipped-test exception.
* Confirm no localization `.resx` changes are included for this task.
* Confirm the final diff contains no plaintext API key, secret, pepper, revocation metadata, or last-used fields.

## Risks And Assumptions

* This is a public package contract change, so downstream services may need a coordinated package update.
* The wire field names are expected to follow existing serializer settings, including camelCase naming where used by Arenal Web Server.
* The HMAC verification value is treated as opaque non-plaintext verification material, not as a reversible secret.
* Existing organization documents should deserialize safely when `ApiKeys` is missing.
* Revocation metadata may be added by a future approved change but is intentionally excluded here.

## Open Questions

None.
