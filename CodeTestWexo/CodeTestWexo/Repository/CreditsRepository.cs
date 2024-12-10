using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using RestSharp;

namespace CodeTestWexo.Repository;

public class CreditsRepository(IRestClientRepository restClientRepository, ILogger<CreditsRepository> logger) : ICreditRepository
{
    public async Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId)
    {
        //Getting the actors related to a specific movieId
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
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
        //Getting the actors related to a specific movieId
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
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

    public async Task<List<CastCredits>> GetActorsBySeriesIdAsync(int seriesId)
    {
        //Getting the actors related to a specific movieId
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/tv/{seriesId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetActorsBySeriesIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<CastCredits>();
        }

        //Deserialize of response content
        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (credits?.Cast == null)
        {
            logger.LogWarning("Deserialization returned null for GetActorsBySeriesIdAsync. GetActorsBySeriesIdAsync is {credits}.", credits);
            return new List<CastCredits>();
        }

        return credits.Cast;    }

    public async Task<List<CrewCredits>> GetCrewsBySeriesIdAsync(int seriesId)
    {
        //Getting the actors related to a specific seriesId
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/tv/{seriesId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetCrewsBySeriesIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<CrewCredits>();
        }

        //Deserialize of response content
        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (credits?.Crew == null)
        {
            logger.LogWarning("Deserialization returned null for GetCrewsBySeriesIdAsync. GetCrewsBySeriesIdAsync is {credits}.", credits);
            return new List<CrewCredits>();
        }

        return credits.Crew;    }
}
