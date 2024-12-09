using CodeTestWexo.Components.Models;
using Microsoft.JSInterop;

namespace CodeTestWexo.Components.Pages
{
    public partial class MovieHomePage
    {
        private List<Genre> genres = new();
        private Dictionary<int, List<Movie>> genreMovies = new();
        private Dictionary<int, int> movieCountsPrGenre = new();
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //Gets all genre
                genres = await genreService.GetGenresAsync();

                //Gets all movies that belongs to a specific genre in the first page. 
                foreach (var genre in genres)
                {
                    var paginatedMovies = await genreService.GetPaginatedMoviesByGenreAsync(genre.Id, 1);
                    genreMovies[genre.Id] = paginatedMovies.Movies;
                    movieCountsPrGenre[genre.Id] = paginatedMovies.TotalResults;
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
            isLoading = false;
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