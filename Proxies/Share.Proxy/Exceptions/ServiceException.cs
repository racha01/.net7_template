
namespace Share.Proxy.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(Exception innerException)
           : base("Unexpected response OR got failed response from Authentication service", innerException) { }
    }
}
