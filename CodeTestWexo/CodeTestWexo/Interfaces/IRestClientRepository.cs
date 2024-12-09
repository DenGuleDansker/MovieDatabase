using RestSharp;

namespace CodeTestWexo.Interfaces;

public interface IRestClientRepository
{
    Task<RestClient> GetClientAsync(string baseUrl);

}