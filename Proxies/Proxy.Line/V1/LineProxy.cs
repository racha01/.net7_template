using Proxy.Line.Core;
using Proxy.Line.Core.Interfaces;
using Proxy.Line.V1.Models;
using Share.Proxy.Core;

namespace Proxy.Line.V1
{
    public class LineProxy : ILineProxy
    {
        private readonly ILineClient _client;

        public LineProxy(ILineClient client)
        {
            _client = client;
        }
        public async Task<LineProfileModel> GetProfileAsync(string accessToken, string userId, CancellationToken cancellationToken)
        {
            string uri = string.Format(LineConstant.Apis.V1.GetProfile, userId);
            var baseRequest = new BaseRequest(uri, HttpMethod.Get) { AccessToken = accessToken };
            return await _client.SendAsync<LineProfileModel>(baseRequest, cancellationToken);
        }
    }
}
