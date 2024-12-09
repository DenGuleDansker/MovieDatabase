using System.Text.Json.Serialization;

namespace CodeTestWexo.Models;

public class MovieResponse
{
    public List<Movie>? Results { get; set; }
    public int? TotalPages { get; set; }
    
    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}