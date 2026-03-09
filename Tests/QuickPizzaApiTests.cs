using System.Net;
using NUnit.Framework;

namespace QuickPizza.ApiTests;

public sealed class QuickPizzaPositiveApiTests
{
    private QuickPizzaApiClient _api = null!;

    private static string BaseUrl =>
        Environment.GetEnvironmentVariable("QUICKPIZZA_BASE_URL")
        ?? "https://quickpizza.grafana.com";

    private static string Token =>
        Environment.GetEnvironmentVariable("QUICKPIZZA_TOKEN")
        ?? "vlae9OngnvbiJia6";

    [SetUp]
    public void Setup()
    {
        _api = new QuickPizzaApiClient(BaseUrl, Token);
    }

    [Test]
    public async Task PostPizza_ReturnsRecommendation_WithPizzaAndId()
    {
        var restrictions = new Restrictions
        {
            MaxCaloriesPerSlice = 800,
            MustBeVegetarian = true,
            ExcludedIngredients = new[] { "anchovies", "bacon" },
            ExcludedTools = Array.Empty<string>(),
            MaxNumberOfToppings = 4,
            MinNumberOfToppings = 2
        };

        var response = await _api.CreatePizzaAsync(restrictions);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Pizza, Is.Not.Null);
        Assert.That(response.Data.Pizza!.Id, Is.GreaterThan(0));
        Assert.That(response.Data.Pizza.Name, Is.Not.Empty);
        Assert.That(response.Data.Calories, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetPizzaById_ReturnsPizza()
    {
        const long id = 1;
        var get = await _api.GetPizzaByIdAsync(id);

        Assert.That(get.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(get.Data, Is.Not.Null);
        Assert.That(get.Data!.Id, Is.EqualTo(id));
        Assert.That(get.Data.Name, Is.Not.Empty);
        Assert.That(get.Data.Ingredients, Is.Not.Null);
        Assert.That(get.Data.Ingredients!.Count, Is.GreaterThan(0));
        Assert.That(get.Data.Dough, Is.Not.Null);
        Assert.That(get.Data.Tool, Is.Not.Empty);
    }

    [TestCase("olive_oil")]
    [TestCase("tomato")]
    [TestCase("mozzarella")]
    [TestCase("topping")]
    public async Task GetIngredientsByType_ReturnsIngredients(string type)
    {
        var response = await _api.GetIngredientsAsync(type);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Ingredients, Is.Not.Null);
        Assert.That(response.Data.Ingredients!.Count, Is.GreaterThan(0));
        Assert.That(response.Data.Ingredients.All(i => !string.IsNullOrWhiteSpace(i.Name)), Is.True);
    }

    [Test]
    public async Task GetDoughs_ReturnsList()
    {
        var response = await _api.GetDoughsAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Doughs, Is.Not.Null);
        Assert.That(response.Data.Doughs!.Count, Is.GreaterThan(0));
        Assert.That(response.Data.Doughs.All(d => !string.IsNullOrWhiteSpace(d.Name)), Is.True);
    }

    [Test]
    public async Task GetTools_ReturnsList()
    {
        var response = await _api.GetToolsAsync();

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Tools, Is.Not.Null);
        Assert.That(response.Data.Tools!.Count, Is.GreaterThan(0));
        Assert.That(response.Data.Tools.All(t => !string.IsNullOrWhiteSpace(t)), Is.True);
    }
}
