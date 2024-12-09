using CodeTestWexo.Models;

namespace CodeTestWexo.Interfaces;

public interface IGenreRepository
{
    Task<List<Genre>> GetGenresAsync();
    Task<PaginatedMovies> GetPaginatedMoviesByGenreAsync(int genreId, int page);

}
