using CodeTestWexo.Models;

namespace CodeTestWexo.Components.Pages;

public partial class TrendingMovie
{
    private MovieTrendingResponse? movies;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
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

    private void NavigateToMovieDetails(int movieId)
    {
        //Redirecting to detailspage
        NavigationManager.NavigateTo($"/movie/{movieId}");
    }
}