using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;

namespace CodeTestWexo.Interfaces;

public interface IMovieRepository
{
    Task<Movie?> GetMovieDetailsAsync(int movieId);
    Task<MovieTrendingResponse?> GetTrendingMoviesAsync();
    Task<List<MovieVideo>?> GetMovieVideosAsync(int movieId);

}