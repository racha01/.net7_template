using System.Text.Json.Serialization;

namespace Proxy.Line.Authorization.Core.Models
{
    public class GetAccessTokenRequest
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; private set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; private set; }

        [JsonPropertyName("grant_type")]
        public string GrantType { get; private set; }

        public GetAccessTokenRequest(string clientId, string clientSecret, string grantType)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            GrantType = grantType;
        }
    }
}
