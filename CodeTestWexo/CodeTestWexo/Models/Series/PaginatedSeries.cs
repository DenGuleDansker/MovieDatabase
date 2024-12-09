using System.Text.Json.Serialization;

namespace CodeTestWexo.Models.Series;

public class PaginatedSeries
{
    public int CurrentPage { get; set; }

    [JsonPropertyName("total_results")] 
    public int TotalResults { get; set; }

    [JsonPropertyName("total_pages")] 
    public int TotalPages { get; set; }
    
    public List<Serie> Series { get; set; } = new List<Serie>();
}