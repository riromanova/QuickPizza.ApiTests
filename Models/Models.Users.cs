using System.Text.Json.Serialization;

namespace QuickPizza.ApiTests;

public sealed class UserRegistrationRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}

public sealed class UserRegistrationResponse
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
}

public sealed class TokenLoginRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("csrf")]
    public string? Csrf { get; set; }
}

public sealed class TokenLoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
}
