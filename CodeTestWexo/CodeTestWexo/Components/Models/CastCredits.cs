using System.Text.Json.Serialization;

namespace CodeTestWexo.Components.Models;


public class CastCredits
{
    public string? Name { get; set; }
    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; set; }
        
    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; set; }
}