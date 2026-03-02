using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class PizzaRecommendation
{
    [JsonPropertyName("pizza")]
    public Pizza? Pizza { get; set; }

    [JsonPropertyName("calories")]
    public int Calories { get; set; }

    [JsonPropertyName("vegetarian")]
    public bool Vegetarian { get; set; }
}
