using System.Text.Json.Serialization;

namespace Proxy.Line.Authorization.Core.Models
{
    public class GetAccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Token { get; set; } = default!;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = default!;
    }
}
