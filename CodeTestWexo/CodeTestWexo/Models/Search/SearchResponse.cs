using System.Text.Json.Serialization;

namespace CodeTestWexo.Models.Search;

public class SearchResponse
{
    
    [JsonPropertyName("results")]
    public List<SearchItem> Results { get; set; } = new();
}