using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace ExampleApp.StarWars;

public class StarWarsApiClient
{
    private readonly HttpClient _httpClient;

    public StarWarsApiClient(
        IHttpClientFactory httpClientFactory,
        IOptions<StarWarsApiOptions> options
    )
    {
        _httpClient = httpClientFactory.CreateClient(nameof(StarWarsApiClient));
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
    }

    public async Task<IReadOnlyList<Film>> GetFilmsAsync()
    {
        var films = await _httpClient.GetFromJsonAsync<IReadOnlyList<Film>>("/api/films");

        if (films == null)
        {
            throw new Exception($"{nameof(films)} is null");
        }

        return films;
    }
}

public record Film
{
    public string Title { get; init; } = "";

    [JsonPropertyName("episode_id")]
    public int EpisodeNumber { get; init; }
    public DateOnly ReleaseDate { get; init; }
}
