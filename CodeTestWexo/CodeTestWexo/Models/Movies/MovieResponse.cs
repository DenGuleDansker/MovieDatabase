using System.Text.Json.Serialization;
using CodeTestWexo.Models.Series;

namespace CodeTestWexo.Models.Movies;

public class MovieResponse
{
    public List<Movie>? Results { get; set; }
    public int? TotalPages { get; set; }
    
    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}