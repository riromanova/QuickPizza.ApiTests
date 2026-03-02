using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class Pizza
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("dough")]
    public Dough? Dough { get; set; }

    [JsonPropertyName("ingredients")]
    public List<Ingredient>? Ingredients { get; set; }

    [JsonPropertyName("tool")]
    public string Tool { get; set; } = string.Empty;
}
