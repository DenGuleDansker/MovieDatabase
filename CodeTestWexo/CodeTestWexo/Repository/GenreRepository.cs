using System.Text.Json;
using CodeTestWexo.Interfaces;
using CodeTestWexo.Models;
using RestSharp;

namespace CodeTestWexo.Repository;

public class GenreRepository(IRestClient restClient, ILogger<GenreRepository> logger) : IGenreRepository
{

    private int defaultTotalPages = 25;
    
    public async Task<List<Genre>> GetGenresAsync()
    {
        //Making my request
        var request = new RestRequest()
        {
            Resource = "3/genre/movie/list"
        };
        var response = await restClient.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetGenresAsync. Response Content: {ResponseContent}", response?.Content);
            return new List<Genre>();
        }

        //Deserialize of response content
        var genreList = JsonSerializer.Deserialize<GenreResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        //Want to log if the response is null
        if (genreList?.Genres == null)
        {
            logger.LogWarning("Deserialization returned null for GetGenresAsync. GetGenresAsync is {genreList}.", genreList);
            return new List<Genre>();
        }

        //Returning my genres list
        return genreList.Genres;
    }

    public async Task<PaginatedMovies> GetPaginatedMoviesByGenreAsync(int genreId, int page)
    {
        
        //Making my request
        var request = new RestRequest()
        {
            Resource = $"3/discover/movie?with_genres={genreId}&page={page}"
        };
        var response = await restClient.GetAsync(request);

        if (response?.Content == null)
        {
            logger.LogError("API response content is null for GetPaginatedMoviesByGenreAsync. Response Content: {ResponseContent}", response?.Content);
            return new PaginatedMovies();
        }

        //Deserialize of response content
        var movieList = JsonSerializer.Deserialize<MovieResponse>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        //Want to log if the response is null
        if (movieList == null)
        {
            logger.LogWarning("Deserialization returned null for GetPaginatedMoviesByGenreAsync. GetPaginatedMoviesByGenreAsync is {movieList}.", movieList);
            return new PaginatedMovies();
        }

        //Creating a new instance. 
        var paginatedMovies = new PaginatedMovies();
        paginatedMovies.CurrentPage = page;
        paginatedMovies.TotalResults = movieList.TotalResults;
        paginatedMovies.TotalPages = defaultTotalPages;

        //Return empty list if null
        if (movieList.Results == null)
        {
            logger.LogWarning("Movie results returned null for GetPaginatedMoviesByGenreAsync. movieList is {movieListResults}.", movieList.Results);
            return new PaginatedMovies();
        }
        paginatedMovies.Movies = movieList.Results;

        //returning pagination
        return paginatedMovies;

    }
}
