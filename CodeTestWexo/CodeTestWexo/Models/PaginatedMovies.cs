using System.Text.Json.Serialization;

namespace CodeTestWexo.Models;

public class PaginatedMovies
{
    public int CurrentPage { get; set; }

    [JsonPropertyName("total_results")] 
    public int TotalResults { get; set; }

    [JsonPropertyName("total_pages")] 
    public int TotalPages { get; set; }
    
    public List<Movie> Movies { get; set; } = new List<Movie>();
}