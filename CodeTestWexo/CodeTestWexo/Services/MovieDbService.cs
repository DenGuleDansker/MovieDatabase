using RestSharp;
using System.Text.Json;
using System.Text.Json.Serialization;

public class MovieDbService
{
    private readonly IConfiguration _configuration;

    public MovieDbService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<Genre>> GetGenresAsync()
    {
        var options = new RestClientOptions("https://api.themoviedb.org/3/genre/movie/list");
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("accept", "application/json");

        var token = _configuration["MovieDb:BearerToken"];
        request.AddHeader("Authorization", $"Bearer {token}");

        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return new List<Genre>();

        var genreList = JsonSerializer.Deserialize<GenreList>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return genreList?.Genres ?? new List<Genre>();
    }

    public async Task<List<Movie>> GetMoviesByGenreAsync(int genreId, int page = 1)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?with_genres={genreId}&page={page}");
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("accept", "application/json");

        var token = _configuration["MovieDb:BearerToken"];
        request.AddHeader("Authorization", $"Bearer {token}");

        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return new List<Movie>();

        var movieList = JsonSerializer.Deserialize<MovieList>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (movieList?.Results == null)
            return new List<Movie>();

        // Collect movies from all pages if necessary
        var allMovies = new List<Movie>(movieList.Results);
        var totalPages = movieList.TotalPages;

        // Fetch movies from subsequent pages if there are more pages
        for (int i = 2; i <= totalPages; i++)
        {
            options = new RestClientOptions($"https://api.themoviedb.org/3/discover/movie?with_genres={genreId}&page={i}");
            client = new RestClient(options);
            response = await client.GetAsync(request);
    
            if (response?.Content != null)
            {
                var additionalMovies = JsonSerializer.Deserialize<MovieList>(response.Content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                allMovies.AddRange(additionalMovies?.Results ?? new List<Movie>());
            }
        }

        return allMovies;
    }



    
    public async Task<Movie> GetMovieDetailsAsync(int movieId)
    {
        var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{movieId}");
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddHeader("accept", "application/json");

        var token = _configuration["MovieDb:BearerToken"];
        request.AddHeader("Authorization", $"Bearer {token}");

        var response = await client.GetAsync(request);
        if (response?.Content == null)
            return null;

        var movieDetails = JsonSerializer.Deserialize<Movie>(response.Content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return movieDetails;
    }

    public class GenreList
    {
        public List<Genre> Genres { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MovieList
    {
        public List<Movie> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }


    public class Movie
    {
        public int Id { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        
        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; }  // This will store the full genre data

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }
        
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
        
        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("vote_average")]

        public double VoteAverage { get; set; }
        [JsonPropertyName("vote_count")]

        public int VoteCount { get; set; }
    }


}
