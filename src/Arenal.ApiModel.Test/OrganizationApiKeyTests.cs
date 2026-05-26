using Skyware.Arenal.ApiModel.Model;
using System.Text.Json;

namespace ModelTests;

public class OrganizationApiKeyTests
{

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Test]
    public void OrganizationApiKeys_DefaultsToEmptyCollection()
    {
        var organization = new Organization();

        Assert.That(organization.ApiKeys, Is.Not.Null);
        Assert.That(organization.ApiKeys, Is.Empty);
    }

    [Test]
    public void OrganizationApiKey_HasExpectedPublicContract()
    {
        var apiKey = new OrganizationApiKey()
        {
            Id = "key-1",
            ClientId = "integration-client",
            KeyHash = "hmac-verification-value",
            IsActive = true,
            Created = new DateTime(2026, 5, 26, 12, 0, 0, DateTimeKind.Utc),
            Expires = new DateTime(2026, 6, 26, 12, 0, 0, DateTimeKind.Utc)
        };

        Assert.Multiple(() =>
        {
            Assert.That(apiKey.Id, Is.EqualTo("key-1"));
            Assert.That(apiKey.ClientId, Is.EqualTo("integration-client"));
            Assert.That(apiKey.KeyHash, Is.EqualTo("hmac-verification-value"));
            Assert.That(apiKey.IsActive, Is.True);
            Assert.That(apiKey.Created, Is.EqualTo(new DateTime(2026, 5, 26, 12, 0, 0, DateTimeKind.Utc)));
            Assert.That(apiKey.Expires, Is.EqualTo(new DateTime(2026, 6, 26, 12, 0, 0, DateTimeKind.Utc)));
        });
    }

    [Test]
    public void OrganizationApiKey_SerializedShapeIncludesApprovedFieldsOnly()
    {
        var organization = new Organization()
        {
            ApiKeys = new[]
            {
                new OrganizationApiKey()
                {
                    Id = "key-1",
                    ClientId = "integration-client",
                    KeyHash = "hmac-verification-value",
                    IsActive = true,
                    Created = new DateTime(2026, 5, 26, 12, 0, 0, DateTimeKind.Utc),
                    Expires = new DateTime(2026, 6, 26, 12, 0, 0, DateTimeKind.Utc)
                }
            }
        };

        string json = JsonSerializer.Serialize(organization, JsonOptions);

        using JsonDocument document = JsonDocument.Parse(json);
        JsonElement apiKey = document.RootElement.GetProperty("apiKeys")[0];

        Assert.Multiple(() =>
        {
            Assert.That(apiKey.TryGetProperty("id", out _), Is.True);
            Assert.That(apiKey.TryGetProperty("clientId", out _), Is.True);
            Assert.That(apiKey.TryGetProperty("keyHash", out _), Is.True);
            Assert.That(apiKey.TryGetProperty("isActive", out _), Is.True);
            Assert.That(apiKey.TryGetProperty("created", out _), Is.True);
            Assert.That(apiKey.TryGetProperty("expires", out _), Is.True);

            Assert.That(apiKey.TryGetProperty("key", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("apiKey", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("secret", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("pepper", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("revoked", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("revokedAt", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("revokedBy", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("lastUsed", out _), Is.False);
            Assert.That(apiKey.TryGetProperty("lastUsedAt", out _), Is.False);
        });
    }

    [Test]
    public void OrganizationApiKey_DoesNotExposeSecretRevocationOrUsageFields()
    {
        HashSet<string> propertyNames = typeof(OrganizationApiKey)
            .GetProperties()
            .Select(property => property.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        Assert.Multiple(() =>
        {
            Assert.That(propertyNames, Does.Contain(nameof(OrganizationApiKey.Id)));
            Assert.That(propertyNames, Does.Contain(nameof(OrganizationApiKey.ClientId)));
            Assert.That(propertyNames, Does.Contain(nameof(OrganizationApiKey.KeyHash)));
            Assert.That(propertyNames, Does.Contain(nameof(OrganizationApiKey.IsActive)));
            Assert.That(propertyNames, Does.Contain(nameof(OrganizationApiKey.Created)));
            Assert.That(propertyNames, Does.Contain(nameof(OrganizationApiKey.Expires)));

            Assert.That(propertyNames, Does.Not.Contain("Key"));
            Assert.That(propertyNames, Does.Not.Contain("ApiKey"));
            Assert.That(propertyNames, Does.Not.Contain("Secret"));
            Assert.That(propertyNames, Does.Not.Contain("Pepper"));
            Assert.That(propertyNames, Does.Not.Contain("Revoked"));
            Assert.That(propertyNames, Does.Not.Contain("RevokedAt"));
            Assert.That(propertyNames, Does.Not.Contain("RevokedBy"));
            Assert.That(propertyNames, Does.Not.Contain("LastUsed"));
            Assert.That(propertyNames, Does.Not.Contain("LastUsedAt"));
        });
    }

    [Test]
    public void Organization_DeserializesMissingApiKeysAsEmptyCollection()
    {
        Organization organization = JsonSerializer.Deserialize<Organization>("{}", JsonOptions)!;

        Assert.That(organization.ApiKeys, Is.Not.Null);
        Assert.That(organization.ApiKeys, Is.Empty);
    }

}
