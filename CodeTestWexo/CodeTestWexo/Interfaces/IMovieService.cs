using CodeTestWexo.Components.Models;

namespace CodeTestWexo.Interfaces;

public interface IMovieService
{
    Task<Movie> GetMovieDetailsAsync(int movieId);
}