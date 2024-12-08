using System.Text.Json;
using CodeTestWexo.Components.Models;
using CodeTestWexo.Interfaces;
using RestSharp;

namespace CodeTestWexo.Services;

public class CreditsService(IRestClientService restClientService) : ICreditService
{
    public async Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId)
    {
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return new List<CastCredits>();

        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return credits?.Cast ?? new List<CastCredits>();
    }
}
