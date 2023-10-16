
namespace Proxy.Line.Authorization.Core.Interfaces
{
    public interface IOAuthLineManager
    {
        Task<string> GetAccessTokenAsync(CancellationToken cancellationToken);
    }
}
