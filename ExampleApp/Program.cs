using ExampleApp;
using ExampleApp.Configuration;
using ExampleApp.StarWars;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// This example demonstrates how to use the Options pattern in a .NET console application to configure
// and consume a simple API client for the Star Wars API (SWAPI).
// The application does not use the Generic Host, but instead manually builds the configuration and service provider.

// Define the configuration sources for the application, including appsettings.json, environment variables, user secrets, and command line arguments.
// The priority of configuration sources is determined by the order they are added, with later sources overriding earlier ones.
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .ConfigureAzureAppConfiguration()
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    .AddCommandLine(args)
    .Build();

// Set up the dependency injection container, registering the necessary services for the application.
var services = new ServiceCollection()
    .Configure<StarWarsApiOptions>(config.GetSection("StarWarsApi"))
    .AddSingleton<IValidateOptions<StarWarsApiOptions>, StarWarsApiOptionsValidator>()
    .AddHttpClient()
    .AddSingleton<StarWarsApiClient>()
    .AddSingleton<ProgramRunner>()
    .BuildServiceProvider();

var runner = services.GetRequiredService<ProgramRunner>();

// Run the application and return the exit code to the operating system.
return await runner.RunAsync();
