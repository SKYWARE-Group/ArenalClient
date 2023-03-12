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
    Person {
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
    Order ||--|| Person : Patient
    Order ||--o{ Service : Services
    Order ||--o{ LinkedReferral : LinkedReferrals
    Order ||--o{ Sample : Samples
    Service ||--|| Identifier : Id
    Person ||--o{ Contact : Contacts
    Person ||--o{ Identifier : Identifiers
    LinkedReferral ||--|| Identifier : Id
    Sample ||--|| Identifier : TypeId
    Sample ||--|| Identifier : AdditiveId
    Sample ||--|| Identifier : Id
```

## Identifiers

| Authority | Identfier type | Applicable country | Dictionaries | Notes |
| :---  | :---  | :---  | :---  | :---  |
| `bg.bma` | УИН | Bulgaria | None | |
| `bg.brra` | ЕИК | Bulgaria | None | |
| `bg.grao` | ЕГН | Bulgaria | None | |
| `bg.his` | Разни | Bulgaria | Номенклатури:<br>null/empty<br>`CL022`<br>`CL024` | Usage:<br>НРН<br>Ordered test<br>Test result identification |
| `org.hl7` | HL7 code | All | HL7 tables:<br>`0487` | Usage:<br> Sample type |
| `bg.mi` | ЛНЧ | Bulgaria | None |  |
| `bg.mh` | РЦЗ/РЗИ код | Bulgaria | None | |
| `bg.nhis` | Продукт/ЛЗ | Bulgaria | `prod`, `org` | При речник `prod` - код на изследване, кл. пътека и др., при `org` - НЗОК номер на ЛЗ |
| `eu.vies` | EU VAT number | EU countries | None | |
| `org.loinc` | Loinc code | All | None | Use for test identification |


