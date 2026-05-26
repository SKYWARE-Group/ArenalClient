# <img src="ArenalApiModel/Assets/logo-128-nuget.png" height="48"> Arenal Client

[![Nuget Badge](https://img.shields.io/nuget/v/Skyware.Arenal.Model)](https://www.nuget.org/packages/Skyware.Arenal.Model)
![Nuget Badge](https://img.shields.io/github/actions/workflow/status/SKYWARE-Group/ArenalClient/dotnet.yml)

This project is a .NET data model and web client for [Arenal](https://awp.skyware-group.com/) service.

For documentation, see [Wiki](https://github.com/SKYWARE-Group/ArenalClient/wiki).

## AI-Native Development

Repository-specific agent instructions live in [docs/agents](docs/agents/). Before asking an AI coding agent to change code, start from the process in [docs/dev-process](docs/dev-process/README.md): raw issues and intents become reviewable Change Requests or ADRs, then approved tasks, then code and tests.

Useful local verification:

```powershell
dotnet test .\src\Arenal.ApiModel.Test\Arenal.ApiModel.Test.csproj
dotnet test .\ArenalClient.slnx
```
