# <img src="ArenalApiModel/Assets/logo-128-nuget.png" height="48"> Arenal Client

[![Nuget Badge](https://img.shields.io/nuget/v/Skyware.Arenal.Model)](https://www.nuget.org/packages/Skyware.Arenal.Model)
![Nuget Badge](https://img.shields.io/github/actions/workflow/status/SKYWARE-Group/ArenalClient/dotnet.yml)

This project is a .NET data model and web client for [Arenal](https://awp.skyware-group.com/) service.

## Order Model

```mermaid
erDiagram
    Order {
        string ArenalId
        string PlacerId
        string PlacerOrderId
        string ProviderId
        string ProviderOrderId
        var other
    }
    Identifier {
        string Authority
        string Dictionary
        string Value
    }
    Patient {
        var other
    }
    Service {
        var other
    }
    LinkedReferral {
        var other
    }
    Sample {
        var other
    }
    Contact {
        var other
    }
    Order ||--|| Patient : Patient
    Order ||--o{ Service : Services
    Order ||--o{ LinkedReferral : LinkedReferrals
    Order ||--o{ Sample : Samples
    Service ||--|| Identifier : Id
    Patient ||--o{ Contact : Contacts
    Patient ||--o{ Identifier : Identifiers
    LinkedReferral ||--|| Identifier : Id
    Sample ||--|| Identifier : TypeId
    Sample ||--|| Identifier : Id
```
