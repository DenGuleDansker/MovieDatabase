using CodeTestWexo.Models;

namespace CodeTestWexo.Interfaces;

public interface IMovieRepository
{
    Task<Movie?> GetMovieDetailsAsync(int movieId);
    Task<MovieTrendingResponse?> GetTrendingMoviesAsync();
    Task<List<Video>?> GetMovieVideosAsync(int movieId);

}