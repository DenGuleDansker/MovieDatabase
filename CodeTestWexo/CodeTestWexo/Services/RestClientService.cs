using RestSharp;

namespace CodeTestWexo.Services;

public class RestClientService(IConfiguration configuration) : IRestClientService
{
    public async Task<RestClient> GetClientAsync(string baseUrl)
    {
        var options = new RestClientOptions(baseUrl);
        var client = new RestClient(options);

        var token = configuration["MovieDb:BearerToken"];
        client.AddDefaultHeader("Authorization", $"Bearer {token}");
        client.AddDefaultHeader("accept", "application/json");

        return client;
    }
}