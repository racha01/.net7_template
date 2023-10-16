
using Share.Proxy.Core;

namespace Proxy.Line.Core.Interfaces
{
    public interface ILineClient
    {
        Task<string> SendAsync(BaseRequest request, CancellationToken cancellationToken);
        Task<T> SendAsync<T>(BaseRequest request, CancellationToken cancellationToken);
    }
}
