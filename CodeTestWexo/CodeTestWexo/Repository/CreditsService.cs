﻿using System.Text.Json;
using CodeTestWexo.Components.Models;
using CodeTestWexo.Interfaces;
using RestSharp;

namespace CodeTestWexo.Repository;

public class CreditsService(IRestClientService restClientService, ILogger<CreditsService> logger) : ICreditService
{
    public async Task<List<CastCredits>> GetActorsByMovieIdAsync(int movieId)
    {
        //Getting the actors related to a specific movieId
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetActorsByMovieIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<CastCredits>();
        }

        //Deserialize of response content
        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (credits?.Cast == null)
        {
            logger.LogWarning("Deserialization returned null for GetTrendingMoviesAsync. GetActorsByMovieIdAsync is {credits}.", credits);
            return new List<CastCredits>();
        }

        return credits.Cast;
    }
    
    public async Task<List<CrewCredits>> GetCrewsByMovieIdAsync(int movieId)
    {
        //Getting the actors related to a specific movieId
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/movie/{movieId}/credits");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetActorsByMovieIdAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<CrewCredits>();
        }

        //Deserialize of response content
        var credits = JsonSerializer.Deserialize<CreditsResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (credits?.Crew == null)
        {
            logger.LogWarning("Deserialization returned null for GetTrendingMoviesAsync. GetActorsByMovieIdAsync is {credits}.", credits);
            return new List<CrewCredits>();
        }

        return credits.Crew;
    }
}