using CodeTestWexo.Interfaces;
using RestSharp;

namespace CodeTestWexo.Services;

public class RestClientRepository(IConfiguration configuration) : IRestClientRepository
{
    
    //Using this RestClient for recieving Json responses, while i want to fetch the API
    public Task<RestClient> GetClientAsync(string baseUrl)
    {
        var options = new RestClientOptions(baseUrl);
        var client = new RestClient(options);

        //Getting the bearerToken from appsettings.json
        var token = configuration["MovieDb:BearerToken"];
        client.AddDefaultHeader("Authorization", $"Bearer {token}");
        client.AddDefaultHeader("accept", "application/json");

        return Task.FromResult(client);
    }
}