
namespace Proxy.Line.Authorization.Exceptions
{
    public class AuthorizationServiceFailedException : Exception
    {
        public int StatusCode { get; }
        public string ResponseContent { get; set; }

        public AuthorizationServiceFailedException(int statusCode, string responseContent) : base($"There is error when call Authorization service: {statusCode} - {responseContent}")
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }
    }
}
