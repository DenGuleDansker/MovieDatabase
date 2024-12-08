using CodeTestWexo.Components.Models;
using Microsoft.AspNetCore.Components;

namespace CodeTestWexo.Components.Pages;

public partial class MovieDetails
{
    [Parameter] public int movieId { get; set; }
    private Movie? movie;
    private List<Video> videos = new();
    private List<CastCredits> actors = new List<CastCredits>();
    private List<int> wishlist = new List<int>(); // Store movie IDs that are in the wishlist

    protected override async Task OnInitializedAsync()
    {
        // Fetch movie details
        movie = await MovieService.GetMovieDetailsAsync(movieId);

        // Fetch movie actors
        actors = await CreditsService.GetActorsByMovieIdAsync(movieId);

        //Fetch Trailers 
        videos = await MovieService.GetMovieVideosAsync(movieId);

        // Retrieve the current wishlist from localStorage
        wishlist = await localStorage.GetItemAsync<List<int>>("wishlist") ?? new List<int>();
    }

    private async Task ToggleWishlist()
    {
        if (movie == null)
        {
            return; // Exit early if movie is null
        }

        if (wishlist.Contains(movie.Id))
        {
            wishlist.Remove(movie.Id); // Remove movie from wishlist
        }
        else
        {
            wishlist.Add(movie.Id); // Add movie to wishlist
        }

        // Save the updated wishlist to localStorage
        await localStorage.SetItemAsync("wishlist", wishlist);
    }

    private string GetButtonClass()
    {
        if (movie == null)
        {
            return string.Empty; // Return a default value if movie is null
        }

        if (wishlist.Contains(movie.Id))
        {
            return "red-btn"; // Movie is in the wishlist
        }

        return "green-btn"; // Movie is not in the wishlist
    }
}