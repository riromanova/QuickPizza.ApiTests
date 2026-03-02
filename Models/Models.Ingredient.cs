using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class Ingredient
{
    [JsonPropertyName("ID")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("caloriesPerSlice")]
    public int CaloriesPerSlice { get; set; }

    [JsonPropertyName("vegetarian")]
    public bool Vegetarian { get; set; }
}
