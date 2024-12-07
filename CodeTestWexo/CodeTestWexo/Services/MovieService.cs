﻿using System.Text.Json;
using CodeTestWexo.Components.Models;
using CodeTestWexo.Interfaces;
using RestSharp;

namespace CodeTestWexo.Services;
public class MovieService(IRestClientService restClientService, ILogger<MovieService> logger) : IMovieService
{
    public async Task<Movie?> GetMovieDetailsAsync(int movieId)
    {
        //DI for using the RestClient, less of the same code. 
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}");
        var request = new RestRequest();
        var response = await client.GetAsync(request);
        if (response.Content == null)
        {
            logger.LogError("GetMovieDetailsAsync returned null for movieId: {MovieId}", movieId);
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
            logger.LogWarning("Deserialization returned null for movieId: {MovieId}", movieId);
            return null;
        }

        return movieDetails;
    }

    public async Task<MovieTrendingResponse?> GetTrendingMoviesAsync()
    {
        //DI for using the RestClient, less of the same code. 
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/popular");
        var request = new RestRequest();
        var response = await client.GetAsync(request);
        
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
            logger.LogWarning("Deserialization returned null for GetTrendingMoviesAsync. MovieTrendingResponse is {MovieTrendingResponse}.", movieDiscoverResponse);
        }

        //This does return trending movies
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

        if (videoResponse?.Results == null)
        {
            return new List<Video>();
        }

        return videoResponse.Results;
    }
}