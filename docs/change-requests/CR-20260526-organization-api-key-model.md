# CR-20260526-organization-api-key-model

```yaml
id: CR-20260526-organization-api-key-model
type: change-request
status: Approved
owner: "TBD"
authors:
  humans:
    - "Human"
  agents:
    - "Codex"
source:
  type: intent
  ref: "ArenalWebServer docs/adr/ADR-20260526-api-key-authentication.md"
agent_context:
  agent: "Codex"
  model: "GPT-5"
  tools:
    - "shell"
    - "apply_patch"
created: 2026-05-26
updated: 2026-05-26
```

## Intent

Add the public Arenal API model contract needed for organization-owned API key records so Arenal Web Server can persist API key metadata inside each `Organization` document.

This Change Request is derived from the approved Arenal Web Server API key authentication ADR. That ADR requires API keys to be database-backed, organization-owned, and stored as non-plaintext verification material embedded in each organization.

## Clarified Goal

Extend `Skyware.Arenal.ApiModel.Model.Organization` with an API key list and add a public API key record model that can be serialized in organization documents.

The model must support API key administration, validation, rotation, and expiration metadata without storing plaintext API keys and without tracking last-used timestamps. Revocation-specific metadata is intentionally deferred from the first model change.

## Scope

* Add a public `OrganizationApiKey` model type for organization API key records in `src/Arenal.ApiModel/Model/`.
* Add an `ApiKeys` collection property to `Organization` that defaults to an empty collection.
* Include API key metadata required by the Web Server authentication ADR:
  * key id;
  * client id or display name;
  * non-secret HMAC verification value;
  * active/disabled status;
  * creation timestamp;
  * optional expiration timestamp.
* Add XML documentation for the new public type and property.
* Add or update model/unit tests that verify the public contract can be instantiated and serialized without plaintext secret or `lastUsed` fields.
* Update package documentation or README notes when the new public model contract is externally visible.

## Non-Scope

* Implementing API key authentication in Arenal Web Server.
* Implementing API key generation, hashing, validation, revocation, rotation, or administration endpoints.
* Adding server-side pepper configuration.
* Adding revocation-specific metadata such as `Revoked`, `RevokedAt`, or `RevokedBy` in the first model change.
* Storing plaintext API keys, API key secrets, or server-side pepper values in the model.
* Adding `lastUsed`, `lastUsedAt`, or equivalent usage tracking fields.
* Adding display/localization resources for API key metadata or the `Organization.ApiKeys` collection property.
* Renaming or removing existing public `Organization` members.
* Changing package metadata, package id, or release process.

## Alternatives Considered

| Alternative | Reason not chosen |
| --- | --- |
| Keep API key records only in Arenal Web Server-local models | The organization document is represented by the shared API model package, so server-only shape would drift from the persisted contract. |
| Store API keys in configuration instead of the organization model | Rejected by the approved Web Server ADR because keys must be per-organization and revocable at runtime. |
| Store plaintext API keys in the model | Not acceptable because API keys are bearer secrets and the ADR requires non-plaintext verification material only. |
| Add usage tracking such as `lastUsed` | Explicitly out of scope; API key usage will not be tracked in this model change. |

## Acceptance Criteria

* `Organization` exposes an API key collection suitable for embedding API key records in organization documents.
* A public `OrganizationApiKey` record model exists in `Skyware.Arenal.ApiModel.Model`.
* The API key record model includes non-secret verification material and administration metadata needed by Arenal Web Server.
* `Organization.ApiKeys` defaults to an empty collection while remaining compatible with existing serialized documents that omit API key data.
* The model does not include plaintext key, secret, pepper, revocation-specific metadata, `lastUsed`, or equivalent usage tracking fields.
* Existing `Organization` public members are not renamed or removed.
* Existing organization documents without API key data remain compatible with deserialization.
* XML documentation describes that API key records store metadata and non-plaintext verification material only.
* No display/localization resources are required for API key metadata or the `Organization.ApiKeys` collection property.
* Unit tests or focused contract checks cover the new model shape and guard against introducing plaintext secret or usage tracking fields.
* Package documentation is updated if the new public model surface is documented for consumers.

## Risks And Assumptions

* This is a public package contract change; downstream services may need a coordinated package update before using the new property.
* Adding a collection property to `Organization` should be backward-compatible for existing serialized documents if missing values are treated as empty by the model or null-safe by consumers.
* The exact field names should align with Arenal Web Server JSON expectations, including camelCase wire names under the server's existing serializer settings.
* This CR assumes the API key verification value is stored as an opaque string, not as plaintext or reversible encrypted secret material.
* Revocation metadata may be useful later, but it is intentionally excluded from this first public model change.

## Suggested Implementation Notes

The target shape is conceptually:

```csharp
public class OrganizationApiKey
{
    public string Id { get; set; }

    public string ClientId { get; set; }

    public string KeyHash { get; set; }

    public bool IsActive { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Expires { get; set; }
}
```

`Organization` should expose the records as a collection, for example:

```csharp
public IEnumerable<OrganizationApiKey> ApiKeys { get; set; } = Array.Empty<OrganizationApiKey>();
```

The implementation must use `OrganizationApiKey` for the public record type name, keep the contract aligned with the approved Web Server API key ADR, and must not add usage tracking or revocation-specific metadata in the first model change.

## Test Expectations

* Add fast NUnit tests under `src/Arenal.ApiModel.Test/` when practical.
* Verify the new model type and `Organization.ApiKeys` property are public and usable by package consumers.
* Verify serialized shape includes expected metadata fields and excludes plaintext secret and `lastUsed` style fields.
* Run `dotnet test .\src\Arenal.ApiModel.Test\Arenal.ApiModel.Test.csproj` during implementation.
* Run `dotnet test .\ArenalClient.slnx` before PR unless the owner explicitly accepts a skipped-test exception.

## Resolved Decisions

* The public record type must be named `OrganizationApiKey`.
* Revocation-specific metadata is not included in the first model change.
* `Organization.ApiKeys` should default to an empty collection.
* Display/localization resources are not required for API key metadata or the `Organization.ApiKeys` collection property.

## Open Questions

None.
