using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class Rating
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("stars")]
    public int Stars { get; set; }

    [JsonPropertyName("pizza_id")]
    public long PizzaId { get; set; }
}
