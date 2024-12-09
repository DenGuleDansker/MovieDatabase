using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using CodeTestWexo.Models.Series;

namespace CodeTestWexo.Components.Pages;

public partial class Wishlist
{
    private List<Serie> serieWishList = new List<Serie>();
    private List<Movie> movieWishList = new List<Movie>();

    protected override async Task OnInitializedAsync()
    {
        // Retrieve the movie IDs from localStorage
        var movieIds = await localStorage.GetItemAsync<List<int>>("moviewishlist") ?? new List<int>();
        
        var serieIds = await localStorage.GetItemAsync<List<int>>("seriewishlist") ?? new List<int>();
        
        // Fetch the movie details for each movieId
        foreach (var movieId in movieIds)
        {
            var movie = await MovieRepository.GetMovieDetailsAsync(movieId);
            if (movie != null)
            {
                movieWishList.Add(movie); // Add movie to the wishlist
            }
        }
        
        foreach (var serieId in serieIds)
        {
            var serie = await SerieRepository.GetSerieDetailsAsync(serieId);
            if (serie != null)
            {
                serieWishList.Add(serie); // Add movie to the wishlist
            }
        }
        
        StateHasChanged();
    }

    private async Task RemoveMovieFromWishlist(int movieId)
    {
        // Remove movie from the wishlist list
        var movieToRemove = movieWishList.FirstOrDefault(m => m.Id == movieId);
        if (movieToRemove != null)
        {
            movieWishList.Remove(movieToRemove);

            // Remove movieId from localStorage
            var movieIds = await localStorage.GetItemAsync<List<int>>("moviewishlist") ?? new List<int>();
            movieIds.Remove(movieId);

            // Save the updated wishlist to localStorage
            await localStorage.SetItemAsync("moviewishlist", movieIds);
        }
    }
    private async Task RemoveSerieFromWishlist(int serieId)
    {
        // Remove movie from the wishlist list
        var serieToRemove = serieWishList.FirstOrDefault(m => m.Id == serieId);
        if (serieToRemove != null)
        {
            serieWishList.Remove(serieToRemove);

            // Remove movieId from localStorage
            var movieIds = await localStorage.GetItemAsync<List<int>>("seriewishlist") ?? new List<int>();
            movieIds.Remove(serieId);

            // Save the updated wishlist to localStorage
            await localStorage.SetItemAsync("seriewishlist", movieIds);
        }
    }

    private void RedirectToMoviePage(int movieId)
    {
        // Redirect to the movie details page
        Navigation.NavigateTo($"/movie/{movieId}");
    }
    
    private void RedirectToSeriePage(int serieId)
    {
        // Redirect to the movie details page
        Navigation.NavigateTo($"/serie/{serieId}");
    }
}