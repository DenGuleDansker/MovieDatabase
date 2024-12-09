using System.Text.Json.Serialization;

namespace CodeTestWexo.Models.Series;

public class Serie
{
    public int Id { get; set; }

    public string? Overview { get; set; } 
    
    public double Popularity { get; set; }
        
    [JsonPropertyName("genres")]
    public List<Genre>? Genres { get; set; }  

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; set; }
        
    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }
        
    [JsonPropertyName("release_date")]
    public DateTime ReleaseDate { get; set; }
    
    [JsonPropertyName("name")]
    public string? Title { get; set; }
    
    [JsonPropertyName("vote_average")]
    
    public double VoteAverage { get; set; }
    [JsonPropertyName("vote_count")]

    public int VoteCount { get; set; }
}