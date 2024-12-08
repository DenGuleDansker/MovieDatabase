using CodeTestWexo.Components.Models;

namespace CodeTestWexo.Interfaces;

public interface IMovieService
{
    Task<Movie> GetMovieDetailsAsync(int movieId);
    Task<MovieTrendingResponse> GetTrendingMoviesAsync();
    Task<List<Video>> GetMovieVideosAsync(int movieId);

}