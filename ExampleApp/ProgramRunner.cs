using ExampleApp.StarWars;

namespace ExampleApp;

public class ProgramRunner(StarWarsApiClient starWarsApiClient)
{
    public async Task<int> RunAsync()
    {
        var films = await starWarsApiClient.GetFilmsAsync();

        foreach (var film in films)
        {
            Console.WriteLine($"Episode {film.EpisodeNumber}: {film.Title}");
        }

        return 0;
    }
}
