using CodeTestWexo.Models;

namespace CodeTestWexo.Interfaces;

public interface IGenreService
{
    Task<List<Genre>> GetGenresAsync();

    Task<PaginatedMovies> GetPaginatedMoviesByGenreAsync(int genreId, int page);

}