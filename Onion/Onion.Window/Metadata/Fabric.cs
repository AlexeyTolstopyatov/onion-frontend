using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Onion.Window.Metadata;

public class Fabric
{
    [JsonPropertyName("id")] 
    public string? Id { get; set; }
    [JsonPropertyName("name")] 
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("license")]
    public string? License { get; set; }
    [JsonPropertyName("version")]
    public string? Version { get; set; }
    [JsonPropertyName("depends")]
    public Dictionary<string, string> Depends { get; set; } = new();
}