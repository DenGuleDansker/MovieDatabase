using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;

namespace CodeTestWexo.Components.Pages;

public partial class Wishlist
{
    private List<Movie> wishlist = new List<Movie>();

    protected override async Task OnInitializedAsync()
    {
        // Retrieve the movie IDs from localStorage
        var movieIds = await localStorage.GetItemAsync<List<int>>("wishlist") ?? new List<int>();
        
        // Fetch the movie details for each movieId
        foreach (var movieId in movieIds)
        {
            var movie = await MovieRepository.GetMovieDetailsAsync(movieId);
            if (movie != null)
            {
                wishlist.Add(movie); // Add movie to the wishlist
            }
        }
        
        StateHasChanged();
    }

    private async Task RemoveFromWishlist(int movieId)
    {
        // Remove movie from the wishlist list
        var movieToRemove = wishlist.FirstOrDefault(m => m.Id == movieId);
        if (movieToRemove != null)
        {
            wishlist.Remove(movieToRemove);

            // Remove movieId from localStorage
            var movieIds = await localStorage.GetItemAsync<List<int>>("wishlist") ?? new List<int>();
            movieIds.Remove(movieId);

            // Save the updated wishlist to localStorage
            await localStorage.SetItemAsync("wishlist", movieIds);
        }
    }

    private void RedirectToMoviePage(int movieId)
    {
        // Redirect to the movie details page
        Navigation.NavigateTo($"/movie/{movieId}");
    }
}