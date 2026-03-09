using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class IngredientsResponse
{
    [JsonPropertyName("ingredients")]
    public List<Ingredient>? Ingredients { get; set; }
}

public sealed class DoughsResponse
{
    [JsonPropertyName("doughs")]
    public List<Dough>? Doughs { get; set; }
}

public sealed class ToolsResponse
{
    [JsonPropertyName("tools")]
    public List<string>? Tools { get; set; }
}

public sealed class RatingsResponse
{
    [JsonPropertyName("ratings")]
    public List<Rating>? Ratings { get; set; }
}
