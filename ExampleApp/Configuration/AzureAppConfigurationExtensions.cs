using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace ExampleApp.Configuration;

public static class AzureAppConfigurationExtensions
{
    public static IConfigurationBuilder ConfigureAzureAppConfiguration(
        this IConfigurationBuilder builder
    )
    {
        // If environment variable is set, then load configuration from Azure App Configuration, otherwise skip it.
        // This allows us to use Azure App Configuration in production, but not require it for local development.
        var appConfigUrl = Environment.GetEnvironmentVariable("APP_CONFIGURATION_URL");

        if (appConfigUrl == null)
        {
            return builder;
        }

        builder.AddAzureAppConfiguration(options =>
        {
            // DefaultAzureCredential authentication methods are detailed here: https://learn.microsoft.com/en-us/dotnet/api/azure.identity.defaultazurecredential?view=azure-dotnet
            // This will only config from Azure App Configuration with the specified key filters
            options
                .Connect(new Uri(appConfigUrl), new DefaultAzureCredential())
                .Select(keyFilter: "Shared:*", labelFilter: LabelFilter.Null) // Load shared values
                .Select(keyFilter: "Example:*", labelFilter: LabelFilter.Null); // Load program specific values

            // Authenticate to KeyVault for resolving KeyVault references in App Configuration
            options.ConfigureKeyVault(keyVaultOptions =>
            {
                keyVaultOptions.SetCredential(new DefaultAzureCredential());
            });
        });

        return builder;
    }
}
