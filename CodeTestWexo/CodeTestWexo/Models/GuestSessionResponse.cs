using System.Text.Json.Serialization;

namespace CodeTestWexo.Models;

public class GuestSessionResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("guest_session_id")]
    public string GuestSessionId { get; set; }
    
    [JsonPropertyName("expires_at")]
    public string ExpiresAt { get; set; }

}