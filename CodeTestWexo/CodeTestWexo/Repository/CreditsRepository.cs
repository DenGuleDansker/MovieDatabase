using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using RestSharp;

namespace CodeTestWexo.Repository;

public class CreditsRepository(IRestClient restClient, ILogger<CreditsRepository> logger) : ICreditRepository
{
    public async Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId)
    {
        //Making my request
        var request = new RestRequest
        {
            Resource = $"3/movie/{movieId}/credits"
        };

        var response = await restClient.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetActorsByMovieIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<CastCredits>();
        }

        //Deserialize of response content
        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (credits?.Cast == null)
        {
            logger.LogWarning("Deserialization returned null for GetActorsByMovieIdAsync. GetActorsByMovieIdAsync is {credits}.", credits);
            return new List<CastCredits>();
        }

        return credits.Cast;
    }
    
    public async Task<List<CrewCredits>> GetCrewsByMovieIdAsync(int movieId)
    {

        //Making my request
        var request = new RestRequest()
        {
            Resource = $"3/movie/{movieId}/credits"
        };

        var response = await restClient.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetCrewsByMovieIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<CrewCredits>();
        }

        //Deserialize of response content
        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (credits?.Crew == null)
        {
            logger.LogWarning("Deserialization returned null for GetCrewsByMovieIdAsync. GetCrewsByMovieIdAsync is {credits}.", credits);
            return new List<CrewCredits>();
        }

        return credits.Crew;
    }
}
