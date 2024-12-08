using CodeTestWexo.Components.Models;

namespace CodeTestWexo.Components.Pages;

public partial class RandomMoviePicker
{
    private List<Genre> genres = new List<Genre>();
    private int selectedGenreId = 0; // Default to "All genres"
    private double minRating = 6; // Default min rating
    private List<Movie> filteredMovies = new List<Movie>();
    private Movie? randomMovie;
    private bool isLoading = false;
    private bool randomMovieNotFound = false; // Flag to show "try again" message

    protected override async Task OnInitializedAsync()
    {
        try
        {
            isLoading = true;
            genres = await GenreService.GetGenresAsync();
        }
        finally
        {
            isLoading = false;
        }
    }

    private int GenerateRandomNumber()
    {
        Random random = new Random();
        return random.Next(1, 100);
    }

    private async Task PickRandomMovie()
    {
        isLoading = true;

        var movieDiscoverResponse =
            await GenreService.GetPaginatedMoviesByGenreAsync(selectedGenreId, GenerateRandomNumber());

        filteredMovies = movieDiscoverResponse.Movies
            .Where(movie => movie.VoteAverage >= minRating) // Filtering by rating
            .ToList();

        if (filteredMovies.Any())
        {
            randomMovie = filteredMovies[new Random().Next(filteredMovies.Count)];
        }
        else
        {
            randomMovie = null; // Set to null if no movies match
            randomMovieNotFound = true; // Set the flag to show the "try again" message
        }

        isLoading = false;
    }

    private void NavigateToMovieDetails(int movieId)
    {
        NavigationManager.NavigateTo($"/movie/{movieId}");
    }
}