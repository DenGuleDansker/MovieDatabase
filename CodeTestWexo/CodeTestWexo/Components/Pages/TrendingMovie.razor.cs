using CodeTestWexo.Components.Models;

namespace CodeTestWexo.Components.Pages;

public partial class TrendingMovie
{
    private MovieTrendingResponse? movies;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        // Simply load movies when the page is initialized
        await LoadMovies();
    }

    private async Task LoadMovies()
    {
        // Fetch movie discover data
        movies = await MovieService.GetTrendingMoviesAsync();

        // Stop loading once movies are fetched
        isLoading = false;

        // Ensure UI is updated after data load
        StateHasChanged();
    }

    //Redirecting to detailspage
    private void NavigateToMovieDetails(int movieId)
    {
        NavigationManager.NavigateTo($"/movie/{movieId}");
    }
}