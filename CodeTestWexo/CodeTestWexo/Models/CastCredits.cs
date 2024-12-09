using System.Text.Json.Serialization;

namespace CodeTestWexo.Models;


public class CastCredits
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
        
    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; set; }
}