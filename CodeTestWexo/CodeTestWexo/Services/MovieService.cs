using System.Text.Json;
using CodeTestWexo.Components.Models;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Services;
using RestSharp;

public class MovieService(IRestClientService restClientService) : IMovieService
{
    

    public async Task<Movie> GetMovieDetailsAsync(int movieId)
    {
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}");
        var request = new RestRequest();
        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return null;

        var movieDetails = JsonSerializer.Deserialize<Movie>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return movieDetails;
    }
    
}