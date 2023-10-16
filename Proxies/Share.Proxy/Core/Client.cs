
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Common.Exceptions;

namespace Share.Proxy.Core
{
    public class Client
    {
        private readonly ILogger<Client> _logger;
        private readonly HttpClient _client;

        protected Client(ILogger<Client> logger, HttpClient httpClient)
        {
            _logger = logger;
            _client = httpClient;
        }

        public async Task<string> SendAsync(BaseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HttpRequestMessage message = new HttpRequestMessage(request.HttpMethod, request.Url);

                if (string.IsNullOrEmpty(request.UserAgent) == false)
                    message.Headers.Add("User-Agent", request.UserAgent);

                if (string.IsNullOrEmpty(request.AccessToken) == false)
                    message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", request.AccessToken);

                if (string.IsNullOrEmpty(request.PayloadString) == false)
                    message.Content = new StringContent(request.PayloadString, Encoding.UTF8, MediaTypeNames.Application.Json);

                HttpResponseMessage response = await _client.SendAsync(message, cancellationToken);

                string content = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Plain response: {content}");

                return !response.IsSuccessStatusCode ? throw new InternalProjectProxyException((int)response.StatusCode, content) : content;
            }
            catch (InternalProjectProxyException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<T> SendAsync<T>(BaseRequest request, CancellationToken cancellationToken)
        {
            string content = await SendAsync(request, cancellationToken);
            T? responseDto = JsonSerializer.Deserialize<T>(content);

            return responseDto;
        }
    }
}
