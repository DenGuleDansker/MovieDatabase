using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using Microsoft.AspNetCore.Components;

namespace CodeTestWexo.Components.Pages;

public partial class MovieDetails
{
    [Parameter] public int movieId { get; set; }
    private Movie? movie;
    private List<MovieVideo>? videos = new();
    private List<CastCredits> casts = new List<CastCredits>();
    private List<CrewCredits> crews = new List<CrewCredits>();
    private List<int> movieWishList = new List<int>(); 
    private int displayedActorCount = 9; 

    protected override async Task OnInitializedAsync()
    {
        // Fetch movie details
        movie = await MovieRepository.GetMovieDetailsAsync(movieId);

        //Fetch Trailers 
        videos = await MovieRepository.GetMovieVideosAsync(movieId);

        // Fetch movie actors
        casts = await CreditsRepository.GetActorsByMovieIdAsync(movieId);

        //Fetch movie crew
        crews = await CreditsRepository.GetCrewsByMovieIdAsync(movieId);
        
        // Remove duplicates in crews based on their ID
        crews = crews
            .Where(crew => crew.Job != null && crew.Job.Contains("Director", StringComparison.OrdinalIgnoreCase)) // Filter jobs containing "Director"
            .ToList();


    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // Retrieve the current wishlist from localStorage
            movieWishList = await localStorage.GetItemAsync<List<int>>("moviewishlist") ?? new List<int>();

            StateHasChanged(); 
        }
    }

    private async Task ToggleWishlist()
    {
        if (movie == null)
        {
            return; 
        }

        if (movieWishList.Contains(movie.Id))
        {
            movieWishList.Remove(movie.Id); // Remove movie from wishlist
        }
        else
        {
            movieWishList.Add(movie.Id); // Add movie to wishlist
        }

        // Save the updated wishlist to localStorage
        await localStorage.SetItemAsync("moviewishlist", movieWishList);
    }
    
    private void LoadMoreActors()
    {
        // Increase the displayed count by 5 or display all remaining actors
        displayedActorCount = casts.Count;
    }
    private void FoldActors()
    {
        displayedActorCount = 9;
    }

    private string GetButtonClass()
    {
        if (movie == null)
        {
            return string.Empty; // Return a default value if movie is null
        }

        if (movieWishList.Contains(movie.Id))
        {
            return "red-btn"; // Movie is in the wishlist
        }

        return "green-btn"; // Movie is not in the wishlist
    }
}