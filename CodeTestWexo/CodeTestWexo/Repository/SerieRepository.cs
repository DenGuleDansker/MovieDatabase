using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models.Series;
using RestSharp;

namespace CodeTestWexo.Repository;

public class SerieRepository(IRestClientRepository restClientRepository, ILogger<SerieRepository> logger) : ISerieRepository
{
    public async Task<Serie?> GetSerieDetailsAsync(int serieId)
    {
        //DI for using the RestClient, less of the same code. 
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/tv/{serieId}");
        var request = new RestRequest();
        var response = await client.GetAsync(request);
        if (response.Content == null)
        {
            logger.LogError("API response content is null for GetSerieDetailsAsync. Response Content: {ResponseContent}", response.Content);
            return null;
        }

        //Converting JSON into C# objects
        var serieDetails = JsonSerializer.Deserialize<Serie>(response.Content, new JsonSerializerOptions
        {
            //Making sure there won't be an error with the api, if the API doesn't match the casing of my C# property
            PropertyNameCaseInsensitive = true
        });

        if (serieDetails == null)
        {
            logger.LogWarning("Deserialization returned null for GetSerieDetailsAsync. GetSerieDetailsAsync is {movieDetails}.", serieDetails);
            return null;
        }

        return serieDetails;
    }

    public Task<SerieTrendingResponse?> GetTrendingSeriesAsync()
    {
        throw new NotImplementedException();
    }
}