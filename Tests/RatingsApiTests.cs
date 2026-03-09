using System.Net;
using System.Linq;
using NUnit.Framework;

namespace QuickPizza.ApiTests;

public sealed class RatingsPositiveApiTests
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

    private async Task<long> GetExistingRatingIdAsync()
    {
        var list = await _api.GetRatingsAsync();
        Assert.That(list.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(list.Data?.Ratings, Is.Not.Null);
        Assert.That(list.Data!.Ratings!.Count, Is.GreaterThan(0));

        return list.Data.Ratings[0].Id;
    }

    [Test]
    public async Task DeleteAllRatings_WhenPermitted_ReturnsNoContent()
    {
        var response = await _api.DeleteAllRatingsAsync();

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            Assert.Ignore("DELETE /api/ratings is forbidden for this token in this environment.");
        }

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task CreateRating_ReturnsCreated()
    {
        var create = await _api.CreateRatingAsync(new Rating
        {
            Stars = 5,
            PizzaId = 1
        });

        Assert.That(create.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        Assert.That(create.Data, Is.Not.Null);
        Assert.That(create.Data!.Id, Is.GreaterThan(0));
        Assert.That(create.Data.Stars, Is.EqualTo(5));
        Assert.That(create.Data.PizzaId, Is.EqualTo(1));
    }

    [Test]
    public async Task GetRatings_ReturnsOk()
    {
        var list = await _api.GetRatingsAsync();
        Assert.That(list.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(list.Data?.Ratings, Is.Not.Null);
    }

    [Test]
    public async Task GetRatingById_ReturnsOk()
    {
        var ratingId = await GetExistingRatingIdAsync();

        var getById = await _api.GetRatingByIdAsync(ratingId);
        Assert.That(getById.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(getById.Data, Is.Not.Null);
        Assert.That(getById.Data!.Id, Is.EqualTo(ratingId));
    }

    [Test]
    public async Task UpdateRatingById_Put_ReturnsOk_WhenPermitted()
    {
        var ratingId = await GetExistingRatingIdAsync();

        var update = await _api.UpdateRatingAsync(ratingId, new Rating
        {
            Stars = 4,
            PizzaId = 1
        });

        if (update.StatusCode == HttpStatusCode.Forbidden)
        {
            Assert.Ignore("PUT /api/ratings/{id} is forbidden for this token in this environment.");
        }

        Assert.That(update.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(update.Data, Is.Not.Null);
        Assert.That(update.Data!.Id, Is.EqualTo(ratingId));
    }

    [Test]
    public async Task UpdateRatingById_Patch_ReturnsOk_WhenPermitted()
    {
        var ratingId = await GetExistingRatingIdAsync();

        var patch = await _api.PatchRatingAsync(ratingId, new { stars = 3 });

        if (patch.StatusCode == HttpStatusCode.Forbidden)
        {
            Assert.Ignore("PATCH /api/ratings/{id} is forbidden for this token in this environment.");
        }

        Assert.That(patch.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(patch.Data, Is.Not.Null);
        Assert.That(patch.Data!.Id, Is.EqualTo(ratingId));
    }

    [Test]
    public async Task DeleteRatingById_ReturnsNoContent_WhenPermitted()
    {
        var ratingId = await GetExistingRatingIdAsync();

        var delete = await _api.DeleteRatingByIdAsync(ratingId);

        if (delete.StatusCode == HttpStatusCode.Forbidden)
        {
            Assert.Ignore("DELETE /api/ratings/{id} is forbidden for this token in this environment.");
        }

        Assert.That(delete.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
}
