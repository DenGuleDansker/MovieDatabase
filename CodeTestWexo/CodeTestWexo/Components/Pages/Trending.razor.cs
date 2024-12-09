using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using CodeTestWexo.Models.Search;
using CodeTestWexo.Models.Series;
using Microsoft.AspNetCore.Components;

namespace CodeTestWexo.Components.Pages;

public partial class Trending
{
    private MovieTrendingResponse? movies;
    private SerieTrendingResponse? series;
    private bool isLoading = true;
    
    //For search
    private string searchQuery = string.Empty;
    private List<SearchItem>? searchResults;

    protected override async Task OnInitializedAsync()
    {
        await LoadMovies();
    }

    private async Task LoadMovies()
    {
        movies = await MovieRepository.GetTrendingMoviesAsync();

        series = await SerieRepository.GetTrendingSeriesAsync();

        
        isLoading = false;

        StateHasChanged();
    }

    private async Task OnInputSearch(ChangeEventArgs e)
    {
        string query = e.Value.ToString();
        await PerformSearch(query);
    }
    
    // Perform search with a query
    private async Task PerformSearch(string query)
    {
        searchQuery = query;
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            searchResults = new List<SearchItem>();  // Clear results if the search is empty
            return;
        }

        isLoading = true;
        var response = await searchRepository.GetSearchDetailsAsync(searchQuery);
        searchResults = response?.Results?.Where(r => r.MediaType != "person").ToList() ?? new List<SearchItem>();  // Only show movies/series
        isLoading = false;
    }
    
    private void NavigateToDetails(SearchItem item)
    {
        if (item == null)
        {
            Console.WriteLine("Item is null");
            return;
        }

        if (string.IsNullOrEmpty(item.MediaType))
        {
            Console.WriteLine("MediaType is null or empty");
            return;
        }

        if (item.MediaType.Equals("movie", StringComparison.OrdinalIgnoreCase))
        {
            NavigateToMovieDetails(item.Id);
        }
        else if (item.MediaType.Equals("tv", StringComparison.OrdinalIgnoreCase))
        {
            NavigateToSerieDetails(item.Id);
        }
    }
    
    private void NavigateToMovieDetails(int movieId)
    {
        //Redirecting to detailspage
        NavigationManager.NavigateTo($"/movie/{movieId}");
    }
    
    private void NavigateToSerieDetails(int serieId)
    {
        //Redirecting to detailspage
        NavigationManager.NavigateTo($"/serie/{serieId}");
    }
    
    
}