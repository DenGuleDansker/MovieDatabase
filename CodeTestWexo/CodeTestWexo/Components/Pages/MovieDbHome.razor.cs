using CodeTestWexo.Components.Models;

namespace CodeTestWexo.Components.Pages;

public partial class MovieDbHome
{
    private List<Genre> genres = new();
    private Dictionary<int, List<Movie>> genreMovies = new();
    private Dictionary<int, int> genreCounts = new();
    private Dictionary<int, int> currentPages = new();
    private Dictionary<int, int> totalPages = new();
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            genres = await GenreService.GetGenresAsync();

            foreach (var genre in genres)
            {
                var paginatedMovies = await GenreService.GetPaginatedMoviesByGenreAsync(genre.Id, 1);
                genreMovies[genre.Id] = paginatedMovies.Movies;
                genreCounts[genre.Id] = paginatedMovies.TotalResults;
                currentPages[genre.Id] = paginatedMovies.CurrentPage;
                totalPages[genre.Id] = paginatedMovies.TotalPages;
            }
        }
        finally
        {
            isLoading = false;
        }
    }

    private void NavigateToGenre(int genreId)
    {
        NavigationManager.NavigateTo($"/genre/{genreId}");
    }

    private void NavigateToMovieDetails(int movieId)
    {
        NavigationManager.NavigateTo($"/movie/{movieId}");
    }
}