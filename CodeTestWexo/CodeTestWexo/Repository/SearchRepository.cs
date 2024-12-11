using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using CodeTestWexo.Models.Search;
using RestSharp;

namespace CodeTestWexo.Repository;

public class SearchRepository(IRestClient restClient, ILogger<SearchRepository> logger) : ISearchRepository
{

    public async Task<SearchResponse?> GetSearchDetailsAsync(string query)
    {
        //Getting the actors related to a specific movieId
        var request = new RestRequest()
        {
            Resource = $"3/search/multi?query={query}"
        };

        var response = await restClient.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetActorsByMovieIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new SearchResponse();
        }

        //Deserialize of response content
        var search = JsonSerializer.Deserialize<SearchResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (search?.Results == null)
        {
            logger.LogWarning("Deserialization returned null for GetActorsByMovieIdAsync. GetActorsByMovieIdAsync is {credits}.", search);
            return new SearchResponse();
        }

        return search; 
    }
}