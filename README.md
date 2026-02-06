# Console App Config Example (.NET)

A small C#/.NET console application that demonstrates how to use the built-in **.NET configuration** system together with **Azure App Configuration**.

## Where to start

Review the application entry point:

- [`ExampleApp/Program.cs`](ExampleApp/Program.cs) (the configuration setup and consumption are shown here)

## Prerequisites

- .NET SDK (a recent LTS version is recommended)
- (Optional) An Azure App Configuration instance if you want to run against Azure

## Run

```bash
dotnet restore
dotnet run
```

## References

* [Configuration in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration)
* [Options Pattern in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/options)
* [Azure App Configuration](https://learn.microsoft.com/en-us/azure/azure-app-configuration/overview)
