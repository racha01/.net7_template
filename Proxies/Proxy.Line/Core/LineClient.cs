
using Microsoft.Extensions.Logging;
using Proxy.Line.Core.Interfaces;
using Share.Proxy.Core;
using System.Net.Http;

namespace Proxy.Line.Core
{
    public class LineClient : Client, ILineClient
    {
        public LineClient(ILogger<LineClient> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory.CreateClient(nameof(LineClient)))
        {
        }
    }
}
