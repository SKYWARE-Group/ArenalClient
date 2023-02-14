# <img src="ArenalApiModel/Assets/logo-128-nuget.png" height="48"> Arenal Client

[![Nuget Badge](https://img.shields.io/nuget/v/Skyware.Arenal.Model)](https://www.nuget.org/packages/Skyware.Arenal.Model)
![Nuget Badge](https://img.shields.io/github/actions/workflow/status/SKYWARE-Group/ArenalClient/dotnet.yml)

This project is a .NET data model and web client for [Arenal](https://awp.skyware-group.com/) service.

## Model

```mermaid
erDiagram
    Order ||--|| Patient : has
    Order }o--|| Service : Services
    Order }o--|| LinkedReferral : LinkedReferrals
    Order }o--|| Sample : Samples
    Service ||--|| Identifier : has
    Patient }o--|| Identifier : Identifiers
    Patient }o--|| Contact : Contacts
```
