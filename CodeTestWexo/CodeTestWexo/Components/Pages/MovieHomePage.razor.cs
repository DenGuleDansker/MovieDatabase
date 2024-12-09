using CodeTestWexo.Components.Models;
using Microsoft.JSInterop;

namespace CodeTestWexo.Components.Pages
{
    public partial class GenresHomePage
    {
        private List<Genre> genres = new();
        private Dictionary<int, List<Movie>> genreMovies = new();
        private Dictionary<int, int> movieCountsPrGenre = new();
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                genres = await genreService.GetGenresAsync();

                foreach (var genre in genres)
                {
                    var paginatedMovies = await genreService.GetPaginatedMoviesByGenreAsync(genre.Id, 1);
                    genreMovies[genre.Id] = paginatedMovies.Movies;
                    movieCountsPrGenre[genre.Id] = paginatedMovies.TotalResults;
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        private void NavigateToGenre(int genreId)
        {
            navigationManager.NavigateTo($"/genre/{genreId}");
        }

        private void NavigateToMovieDetails(int movieId)
        {
            navigationManager.NavigateTo($"/movie/{movieId}");
        }
    }
}
