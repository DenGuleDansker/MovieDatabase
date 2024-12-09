using System.Text.Json.Serialization;

namespace CodeTestWexo.Models;

public class CrewCredits
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
        
    [JsonPropertyName("job")]
    public string? Job { get; set; }
}