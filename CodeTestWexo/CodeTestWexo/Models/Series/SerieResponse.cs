using System.Text.Json.Serialization;

namespace CodeTestWexo.Models.Series;

public class SerieResponse
{
    public List<Serie>? Results { get; set; }
    public int? TotalPages { get; set; }
    
    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}