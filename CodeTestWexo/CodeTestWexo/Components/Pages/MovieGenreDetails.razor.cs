using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using Microsoft.AspNetCore.Components;

namespace CodeTestWexo.Components.Pages;

public partial class MovieGenreDetails
{
    [Parameter] public int genreId { get; set; }
    private string genreName;
    private PaginatedMovies paginatedMovies = new PaginatedMovies();
    private bool IsPreviousDisabled() => paginatedMovies.CurrentPage <= 1;
    private bool IsNextDisabled() => paginatedMovies.CurrentPage >= paginatedMovies.TotalPages;

    protected override async Task OnInitializedAsync()
    {
        await LoadGenreDetailsAsync(1); // Load first page
    }

    private async Task LoadGenreDetailsAsync(int page)
    {
        // Fetch movies for the selected genre and page
        paginatedMovies = await GenreRepository.GetPaginatedMoviesByGenreAsync(genreId, page);
    }

    private async Task ChangePage(int increment)
    {
        int newPage = paginatedMovies.CurrentPage + increment;
        if (newPage < 1 || newPage > paginatedMovies.TotalPages) return;

        await LoadGenreDetailsAsync(newPage);
    }

    private void NavigateToMovieDetails(int movieId)
    {
        navigationManager.NavigateTo($"/movie/{movieId}");
    }

}