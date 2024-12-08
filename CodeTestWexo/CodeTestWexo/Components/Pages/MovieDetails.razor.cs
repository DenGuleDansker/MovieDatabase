using CodeTestWexo.Components.Models;
using Microsoft.AspNetCore.Components;

namespace CodeTestWexo.Components.Pages;

public partial class MovieDetails
{
    [Parameter] public int movieId { get; set; }
    private Movie movie;
    private List<Actor> actors = new List<Actor>();
    private List<int> wishlist = new List<int>(); // Store movie IDs that are in the wishlist

    protected override async Task OnInitializedAsync()
    {
        // Fetch movie details
        movie = await MovieService.GetMovieDetailsAsync(movieId);

        // Fetch movie actors
        actors = await CreditsService.GetActorsByMovieIdAsync(movieId);

        // Retrieve the current wishlist from localStorage
        wishlist = await localStorage.GetItemAsync<List<int>>("wishlist") ?? new List<int>();
    }

    private async Task ToggleWishlist()
    {
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
        return wishlist.Contains(movie.Id) ? "red-btn" : "green-btn";
    }
}