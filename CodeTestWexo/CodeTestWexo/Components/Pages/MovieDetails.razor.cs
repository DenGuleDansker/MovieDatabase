using CodeTestWexo.Components.Models;
using Microsoft.AspNetCore.Components;

namespace CodeTestWexo.Components.Pages;

public partial class MovieDetails
{
    [Parameter] public int movieId { get; set; }
    private Movie? movie;
    private List<Video>? videos = new();
    private List<CastCredits> casts = new List<CastCredits>();
    private List<CrewCredits> crews = new List<CrewCredits>();
    private List<int> wishList = new List<int>(); 
    private int displayedActorCount = 5; 
    private int displayedCrewCount = 5;  

    protected override async Task OnInitializedAsync()
    {
        // Fetch movie details
        movie = await movieService.GetMovieDetailsAsync(movieId);

        //Fetch Trailers 
        videos = await movieService.GetMovieVideosAsync(movieId);

        // Fetch movie actors
        casts = await creditsService.GetActorsByMovieIdAsync(movieId);

        //Fetch movie crew
        crews = await creditsService.GetCrewsByMovieIdAsync(movieId);

        // Filter the crew list to exclude anyone who is already in the cast list while someone comes two times
        crews = crews.Where(crew => !casts.Any(cast => cast.Id == crew.Id)).ToList();

        // Remove duplicates in crews based on their ID
        crews = crews.GroupBy(crew => crew.Id) // Group by ID
            .Select(group => group.First()) // Take the first occurrence
            .ToList();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Retrieve the current wishlist from localStorage
            wishList = await localStorage.GetItemAsync<List<int>>("wishlist") ?? new List<int>();

            StateHasChanged(); 
        }
    }

    private async Task ToggleWishlist()
    {
        if (movie == null)
        {
            return; 
        }

        if (wishList.Contains(movie.Id))
        {
            wishList.Remove(movie.Id); // Remove movie from wishlist
        }
        else
        {
            wishList.Add(movie.Id); // Add movie to wishlist
        }

        // Save the updated wishlist to localStorage
        await localStorage.SetItemAsync("wishlist", wishList);
    }
    
    private void LoadMoreActors()
    {
        // Increase the displayed count by 5 or display all remaining actors
        displayedActorCount = casts.Count;
    }

    private void LoadMoreCrew()
    {
        // Increase the displayed count by 5 or display all remaining crew
        displayedCrewCount = crews.Count;
    }

    private string GetButtonClass()
    {
        if (movie == null)
        {
            return string.Empty; // Return a default value if movie is null
        }

        if (wishList.Contains(movie.Id))
        {
            return "red-btn"; // Movie is in the wishlist
        }

        return "green-btn"; // Movie is not in the wishlist
    }
}