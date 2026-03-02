using RestSharp;

namespace QuickPizza.ApiTests;

internal sealed class QuickPizzaApiClient
{
    private readonly RestClient _client;

    public QuickPizzaApiClient(string baseUrl, string token)
    {
        var options = new RestClientOptions(baseUrl)
        {
            ThrowOnAnyError = false,
            MaxTimeout = 30_000
        };

        _client = new RestClient(options);
        _client.AddDefaultHeader("Authorization", $"Token {token}");
        _client.AddDefaultHeader("Accept", "application/json");
    }

    public async Task<RestResponse<PizzaRecommendation>> CreatePizzaAsync(Restrictions? restrictions = null)
    {
        var request = new RestRequest("/api/pizza", Method.Post);
        if (restrictions is not null)
        {
            request.AddJsonBody(restrictions);
        }
        return await _client.ExecuteAsync<PizzaRecommendation>(request);
    }

    public async Task<RestResponse<Pizza>> GetPizzaByIdAsync(long id)
    {
        var request = new RestRequest("/api/pizza/{id}", Method.Get)
            .AddUrlSegment("id", id);

        return await _client.ExecuteAsync<Pizza>(request);
    }

    public async Task<RestResponse<IngredientsResponse>> GetIngredientsAsync(string type)
    {
        var request = new RestRequest("/api/ingredients/{type}", Method.Get)
            .AddUrlSegment("type", type);

        return await _client.ExecuteAsync<IngredientsResponse>(request);
    }

    public async Task<RestResponse<DoughsResponse>> GetDoughsAsync()
    {
        var request = new RestRequest("/api/doughs", Method.Get);
        return await _client.ExecuteAsync<DoughsResponse>(request);
    }

    public async Task<RestResponse<ToolsResponse>> GetToolsAsync()
    {
        var request = new RestRequest("/api/tools", Method.Get);
        return await _client.ExecuteAsync<ToolsResponse>(request);
    }
}
