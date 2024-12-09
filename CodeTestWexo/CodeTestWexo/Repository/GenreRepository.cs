using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using CodeTestWexo.Models.Series;
using RestSharp;

namespace CodeTestWexo.Repository;

public class GenreRepository(IRestClientRepository restClientRepository, ILogger<GenreRepository> logger) : IGenreRepository
{
    
    public async Task<List<Genre>> GetMovieGenresAsync()
    {
        var client = await restClientRepository.GetClientAsync("https://api.themoviedb.org/3/genre/movie/list");
        var request = new RestRequest();
        var response = await client.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetMovieGenresAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<Genre>();
        }

        var movieGenreList = JsonSerializer.Deserialize<GenreResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (movieGenreList?.Genres == null)
        {
            logger.LogWarning("Deserialization returned null for GetMovieGenresAsync. GetMovieGenresAsync is {genreList}.", movieGenreList);
            return new List<Genre>();
        }

        return movieGenreList.Genres;
    }
    
    public async Task<List<Genre>> GetSerieGenresAsync()
    {
        var client = await restClientRepository.GetClientAsync("https://api.themoviedb.org/3/genre/tv/list");
        var request = new RestRequest();
        var response = await client.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetSeriesGenresAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<Genre>();
        }

        var serieGenreList = JsonSerializer.Deserialize<GenreResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (serieGenreList?.Genres == null)
        {
            logger.LogWarning("Deserialization returned null for GetSeriesGenresAsync. GetSeriesGenresAsync is {serieGenreList}.", serieGenreList);
            return new List<Genre>();
        }

        return serieGenreList.Genres;
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

    public async Task<PaginatedSeries> GetPaginatedSeriesByGenreAsync(int genreId, int page)
    {
        //Use for pagination later in the frontend
        var defaultTotalPages = 25;
        
        //Use for fetching the showcases for GenresHomePage and for pagination for MovieHomePage
        var client = await restClientRepository.GetClientAsync($"https://api.themoviedb.org/3/discover/tv?with_genres={genreId}&page={page}");
        var request = new RestRequest();
        var response = await client.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetPaginatedSeriesByGenreAsync. Response Content: {ResponseContent}", response?.Content);
            return new PaginatedSeries();
        }

        var serieList = JsonSerializer.Deserialize<SerieResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (serieList == null)
        {
            logger.LogWarning("Deserialization returned null for GetPaginatedSeriesByGenreAsync. GetPaginatedSeriesByGenreAsync is {serieList}.", serieList);
            return new PaginatedSeries();
        }

        var paginatedSeries = new PaginatedSeries();
        paginatedSeries.CurrentPage = page;
        paginatedSeries.TotalResults = serieList.TotalResults;
        paginatedSeries.TotalPages = defaultTotalPages;

        if (serieList.Results == null)
        {
            return new PaginatedSeries();
        }
        paginatedSeries.Series = serieList.Results;

        return paginatedSeries;
    }
}
