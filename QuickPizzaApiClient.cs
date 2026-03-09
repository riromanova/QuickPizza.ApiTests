using System.Net;
using RestSharp;

namespace QuickPizza.ApiTests;

internal sealed class QuickPizzaApiClient
{
    private readonly RestClient _client;

    public QuickPizzaApiClient(string baseUrl, string token, CookieContainer? cookieContainer = null)
    {
        var options = new RestClientOptions(baseUrl)
        {
            ThrowOnAnyError = false,
            MaxTimeout = 30_000,
            CookieContainer = cookieContainer
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

    public async Task<RestResponse<Rating>> CreateRatingAsync(Rating rating)
    {
        var request = new RestRequest("/api/ratings", Method.Post)
            .AddJsonBody(rating);

        return await _client.ExecuteAsync<Rating>(request);
    }

    public async Task<RestResponse<RatingsResponse>> GetRatingsAsync()
    {
        var request = new RestRequest("/api/ratings", Method.Get);
        return await _client.ExecuteAsync<RatingsResponse>(request);
    }

    public async Task<RestResponse> DeleteAllRatingsAsync()
    {
        var request = new RestRequest("/api/ratings", Method.Delete);
        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse<Rating>> GetRatingByIdAsync(long id)
    {
        var request = new RestRequest("/api/ratings/{id}", Method.Get)
            .AddUrlSegment("id", id);

        return await _client.ExecuteAsync<Rating>(request);
    }

    public async Task<RestResponse<Rating>> UpdateRatingAsync(long id, Rating rating)
    {
        var request = new RestRequest("/api/ratings/{id}", Method.Put)
            .AddUrlSegment("id", id)
            .AddJsonBody(rating);

        return await _client.ExecuteAsync<Rating>(request);
    }

    public async Task<RestResponse<Rating>> PatchRatingAsync(long id, object patch)
    {
        var request = new RestRequest("/api/ratings/{id}", Method.Patch)
            .AddUrlSegment("id", id)
            .AddJsonBody(patch);

        return await _client.ExecuteAsync<Rating>(request);
    }

    public async Task<RestResponse> DeleteRatingByIdAsync(long id)
    {
        var request = new RestRequest("/api/ratings/{id}", Method.Delete)
            .AddUrlSegment("id", id);

        return await _client.ExecuteAsync(request);
    }

    public async Task<RestResponse<UserRegistrationResponse>> RegisterUserAsync(UserRegistrationRequest registration)
    {
        var request = new RestRequest("/api/users", Method.Post)
            .AddJsonBody(registration);

        return await _client.ExecuteAsync<UserRegistrationResponse>(request);
    }

    public async Task<RestResponse<TokenLoginResponse>> LoginForTokenAsync(TokenLoginRequest login)
    {
        var request = new RestRequest("/api/users/token/login", Method.Post)
            .AddJsonBody(login);

        return await _client.ExecuteAsync<TokenLoginResponse>(request);
    }

    public async Task<RestResponse> GetCsrfTokenAsync()
    {
        var request = new RestRequest("/api/csrf-token", Method.Post);
        return await _client.ExecuteAsync(request);
    }
}
