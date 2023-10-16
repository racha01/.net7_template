
using Proxy.Line.V1.Models;

namespace Proxy.Line.V1
{
    public interface ILineProxy
    {
        Task<LineProfileModel> GetProfileAsync(string accessToken, string userId, CancellationToken cancellationToken);
    }
}
