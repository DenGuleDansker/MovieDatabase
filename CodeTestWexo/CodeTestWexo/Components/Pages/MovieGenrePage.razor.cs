using CodeTestWexo.Models;
using CodeTestWexo.Models.Movies;
using Microsoft.JSInterop;

namespace CodeTestWexo.Components.Pages
{
    public partial class MovieGenrePage
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
                genres = await GenreRepository.GetMovieGenresAsync();

                //Gets all movies that belongs to a specific genre in the first page. 
                foreach (var genre in genres)
                {
                    var paginatedMovies = await GenreRepository.GetPaginatedMoviesByGenreAsync(genre.Id, 1);
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
            navigationManager.NavigateTo($"/moviegenre/{genreId}");
        }

        private void NavigateToMovieDetails(int movieId)
        {
            navigationManager.NavigateTo($"/movie/{movieId}");
        }
    }
}