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
    public async Task<MovieTrendingResponse> GetTrendingMoviesAsync()
    {
        
        
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/popular");
        var request = new RestRequest();
        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return null;

        var movieDiscoverResponse = JsonSerializer.Deserialize<MovieTrendingResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return movieDiscoverResponse;
    }

    public async Task<List<Video>> GetMovieVideosAsync(int movieId)
    {
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}/videos");
        var request = new RestRequest();
        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return new List<Video>(); // Return an empty list if no videos

        var videoResponse = JsonSerializer.Deserialize<VideoResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return videoResponse?.Results ?? new List<Video>();    }
}