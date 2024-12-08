using System.Text.Json;
using CodeTestWexo.Components.Models;
using CodeTestWexo.Interfaces;
using RestSharp;

namespace CodeTestWexo.Services;

public class GenreService(IRestClientService restClientService) : IGenreService
{
    
    public async Task<List<Genre>> GetGenresAsync()
    {
        var client = await restClientService.GetClientAsync("https://api.themoviedb.org/3/genre/movie/list");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return new List<Genre>();

        var genreList = JsonSerializer.Deserialize<GenreResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return genreList?.Genres ?? new List<Genre>();
    }

    public async Task<PaginatedMovies> GetPaginatedMoviesByGenreAsync(int genreId, int page)
    {
        var client = await restClientService.GetClientAsync($"https://api.themoviedb.org/3/discover/movie?with_genres={genreId}&page={page}");
        var request = new RestRequest();

        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return new PaginatedMovies();

        var movieList = JsonSerializer.Deserialize<MovieResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (movieList == null)
            return new PaginatedMovies();

        var defaultTotalPages = 25;
        
        return new PaginatedMovies
        {
            CurrentPage = page,
            TotalResults = movieList.TotalResults,
            TotalPages = defaultTotalPages, // Use the value provided by the API
            Movies = movieList.Results ?? new List<Movie>()
        };
      
    }
}
