using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using RestSharp;

namespace CodeTestWexo.Repository;

public class GenreRepository(IRestClientRepository restClientRepository, ILogger<GenreRepository> logger) : IGenreRepository
{
    
    public async Task<List<Genre>> GetGenresAsync()
    {
        var client = await restClientRepository.GetClientAsync("https://api.themoviedb.org/3/genre/movie/list");
        var request = new RestRequest();
        var response = await client.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetGenresAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<Genre>();
        }

        var genreList = JsonSerializer.Deserialize<GenreResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (genreList?.Genres == null)
        {
            logger.LogWarning("Deserialization returned null for GetGenresAsync. GetGenresAsync is {genreList}.", genreList);
            return new List<Genre>();
        }

        return genreList.Genres;
    }

    public async Task<PaginatedMovies> GetPaginatedMoviesByGenreAsync(int genreId, int page)
    {
        //Use for pagination later in the frontend
        var defaultTotalPages = 25;
        
        //Use for fetching the showcases for GenresHomePage and for pagination for MovieHomePage
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/discover/movie?with_genres={genreId}&page={page}");
        var request = new RestRequest();
        var response = await client.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetPaginatedMoviesByGenreAsync. Response Content: {ResponseContent}", response?.Content);
            return new PaginatedMovies();
        }

        var movieList = JsonSerializer.Deserialize<MovieResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (movieList == null)
        {
            logger.LogWarning("Deserialization returned null for GetPaginatedMoviesByGenreAsync. GetPaginatedMoviesByGenreAsync is {movieList}.", movieList);
            return new PaginatedMovies();
        }

        var paginatedMovies = new PaginatedMovies();
        paginatedMovies.CurrentPage = page;
        paginatedMovies.TotalResults = movieList.TotalResults;
        paginatedMovies.TotalPages = defaultTotalPages;

        if (movieList.Results == null)
        {
            return new PaginatedMovies();
        }
        paginatedMovies.Movies = movieList.Results;

        return paginatedMovies;

    }
}
