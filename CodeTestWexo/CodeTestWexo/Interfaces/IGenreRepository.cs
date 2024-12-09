using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using CodeTestWexo.Models.Series;

namespace CodeTestWexo.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetMovieGenresAsync();
    Task<List<Genre>> GetSerieGenresAsync();
    Task<PaginatedMovies> GetPaginatedMoviesByGenreAsync(int genreId, int page);
    Task<PaginatedSeries> GetPaginatedSeriesByGenreAsync(int genreId, int page);

}
