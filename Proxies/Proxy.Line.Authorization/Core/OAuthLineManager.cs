using Proxy.Line.Authorization.Core.Models;
using Proxy.Line.Authorization.Exceptions;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Common.AppSettings.TestAPI;
using Microsoft.Extensions.Options;
using Proxy.Line.Authorization.Core.Interfaces;
using System.Net.Http.Headers;
using System.Threading.Channels;

namespace Proxy.Line.Authorization.Core
{
    public class OAuthLineManager : IOAuthLineManager
    {
        private readonly ILogger<OAuthLineManager> _logger;
        private readonly HttpClient _client;
        private readonly AuthorizationLineService _settings;

        public OAuthLineManager(ILogger<OAuthLineManager> logger, IHttpClientFactory clientFactory, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _client = clientFactory.CreateClient(nameof(OAuthLineManager));
            _settings = settings.Value.AuthorizationLineService;
        }

        public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            try
            {

                HttpRequestMessage message = new HttpRequestMessage
                {
                    RequestUri = new Uri(_settings.OAuthUrl, UriKind.Absolute),
                    Method = HttpMethod.Post,
                    //Content = new StringContent(
                    //JsonSerializer.Serialize(new GetAccessTokenRequest(_settings.ClientId, _settings.ClientSecret, _settings.GrantType)),
                    //Encoding.UTF8,
                    //MediaTypeNames.Application.Json)
                };
                var contentList = new List<string>
                {
                    $"grant_type={Uri.EscapeDataString(_settings.GrantType)}",
                    $"client_id={Uri.EscapeDataString(_settings.ClientId)}",
                    $"client_secret={Uri.EscapeDataString(_settings.ClientSecret)}"
                };
                message.Content = new StringContent(string.Join("&", contentList));
                message.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                HttpResponseMessage response = await _client.SendAsync(message, cancellationToken);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new AuthorizationServiceFailedException((int)response.StatusCode, responseContent);
                }

                GetAccessTokenResponse? oauthResponse = JsonSerializer.Deserialize<GetAccessTokenResponse>(responseContent);
                return string.IsNullOrEmpty(oauthResponse.Token) ?
                    throw new AuthorizationServiceFailedException((int)response.StatusCode, responseContent) :
                    oauthResponse.Token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw new AuthorizationServiceException(ex);
            }
        }
    }
}
