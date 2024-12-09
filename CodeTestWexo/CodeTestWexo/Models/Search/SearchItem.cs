using System.Text.Json.Serialization;

namespace CodeTestWexo.Models.Search;

public class SearchItem
{
    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string? Title { get; set; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }

    [JsonPropertyName("media_type")]
    public string? MediaType { get; set; } // Nullable to handle missing values

    
}