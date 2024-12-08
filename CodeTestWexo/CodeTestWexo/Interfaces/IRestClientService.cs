using RestSharp;

namespace CodeTestWexo.Interfaces;

public interface IRestClientService
{
    Task<RestClient> GetClientAsync(string baseUrl);

}