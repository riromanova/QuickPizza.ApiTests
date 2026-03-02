using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class Restrictions
{
    [JsonPropertyName("maxCaloriesPerSlice")]
    public int? MaxCaloriesPerSlice { get; set; }

    [JsonPropertyName("mustBeVegetarian")]
    public bool? MustBeVegetarian { get; set; }

    [JsonPropertyName("excludedIngredients")]
    public string[]? ExcludedIngredients { get; set; }

    [JsonPropertyName("excludedTools")]
    public string[]? ExcludedTools { get; set; }

    [JsonPropertyName("maxNumberOfToppings")]
    public int? MaxNumberOfToppings { get; set; }

    [JsonPropertyName("minNumberOfToppings")]
    public int? MinNumberOfToppings { get; set; }
}
