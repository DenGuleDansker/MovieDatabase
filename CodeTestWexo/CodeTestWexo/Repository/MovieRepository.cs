using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using RestSharp;

namespace CodeTestWexo.Repository;
public class MovieRepository(IRestClient restClient, ILogger<MovieRepository> logger) : IMovieRepository
{
    public async Task<Movie?> GetMovieDetailsAsync(int movieId)
    {
        //DI for using the RestClient, less of the same code. 
        var request = new RestRequest()
        {
            Resource = $"3/movie/{movieId}"
        };
        var response = await restClient.GetAsync(request);
        if (response.Content == null)
        {
            logger.LogError("API response content is null for GetMovieDetailsAsync. Response Content: {ResponseContent}", response.Content);
            return null;
        }

        //Converting JSON into C# objects
        var movieDetails = JsonSerializer.Deserialize<Movie>(response.Content, new JsonSerializerOptions
        {
            //Making sure there won't be an error with the api, if the API doesn't match the casing of my C# property
            PropertyNameCaseInsensitive = true
        });

        if (movieDetails == null)
        {
            logger.LogWarning("Deserialization returned null for GetMovieDetailsAsync. GetMovieDetailsAsync is {movieDetails}.", movieDetails);
            return null;
        }

        return movieDetails;
    }

    public async Task<MovieTrendingResponse?> GetTrendingMoviesAsync()
    {
        //Using the restclient with DI and getting the trending movies by fetching this API 
        var request = new RestRequest()
        {
            Resource = "3/movie/popular"
        };
        var response = await restClient.GetAsync(request);
        
        if (response.Content == null)
        {
            logger.LogError("API response content is null for GetTrendingMoviesAsync. Response Content: {ResponseContent}", response.Content);
            return null;
        }

        
        var movieDiscoverResponse = JsonSerializer.Deserialize<MovieTrendingResponse>(response.Content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        
        if (movieDiscoverResponse == null)
        {
            logger.LogWarning("Deserialization returned null for GetTrendingMoviesAsync. GetTrendingMoviesAsync is {MovieTrendingResponse}.", movieDiscoverResponse);
            return null;
        }

        //Return trending movies
        return movieDiscoverResponse;
    }

    public async Task<List<Video>?> GetMovieVideosAsync(int movieId)
    {
        var request = new RestRequest()
        {
            Resource = $"3/movie/{movieId}/videos"
        };
        var response = await restClient.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetMovieVideosAsync. Response Content: {ResponseContent}", response?.Content);
            return null; // Return an empty list if no videos
        }

        var videoResponse = JsonSerializer.Deserialize<VideoResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (videoResponse?.Results == null)
        {
            logger.LogWarning("Deserialization returned null for GetMovieVideosAsync. GetMovieVideosAsync is {VideoResponse}.", videoResponse);
            return null;
        }

        return videoResponse.Results;
    }
}