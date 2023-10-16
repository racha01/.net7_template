
namespace Proxy.Line.Authorization.Exceptions
{
    public class AuthorizationServiceException : Exception
    {
        public AuthorizationServiceException(Exception innerException) : base("Unexpected response OR got failed response from Authorization service", innerException) { }
    }
}
