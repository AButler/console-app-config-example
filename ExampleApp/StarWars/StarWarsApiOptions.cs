using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace ExampleApp.StarWars;

public record StarWarsApiOptions
{
    public string BaseUrl { get; init; } = "";
}

public class StarWarsApiOptionsValidator : IValidateOptions<StarWarsApiOptions>
{
    public ValidateOptionsResult Validate(string? name, StarWarsApiOptions options)
    {
        // Validate options.BaseUrl is set
        if (string.IsNullOrEmpty(options.BaseUrl))
        {
            return ValidateOptionsResult.Fail("BaseUrl must be provided");
        }

        // Validate options.BaseUrl is a valid absolute URI
        if (!Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out var baseUri))
        {
            return ValidateOptionsResult.Fail("BaseUrl must be a valid absolute URI");
        }

        // Validate options.BaseUrl uses HTTPS
        if (baseUri.Scheme != Uri.UriSchemeHttps)
        {
            return ValidateOptionsResult.Fail("BaseUrl must use HTTPS");
        }

        return ValidateOptionsResult.Success;
    }
}
