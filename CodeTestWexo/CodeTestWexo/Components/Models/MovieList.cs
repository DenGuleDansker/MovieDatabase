using System.Text.Json.Serialization;

namespace CodeTestWexo.Components.Models;

public class MovieList
{
    public List<Movie> Results { get; set; }
    public int TotalPages { get; set; }
    
    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}