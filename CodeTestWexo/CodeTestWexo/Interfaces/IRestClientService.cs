using RestSharp;

namespace CodeTestWexo.Services;

public interface IRestClientService
{
    Task<RestClient> GetClientAsync(string baseUrl);

}