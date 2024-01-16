using System.Text.Json.Serialization;

namespace ApiConsorcio.Models;

public class User
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("company")]
    public string Company { get; set; }
}
