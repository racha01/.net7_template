using Common.Model;
using System.Text.Json;

namespace Common.Exceptions
{
    public class InternalProjectProxyException : Exception
    {
        public int StatusCode { get; }
        public string ResponseContent { get; set; }

        public InternalProjectProxyException(int statusCode, string responseContent)
            : base($"There is error when call internal service: {statusCode} - {responseContent}")
        {
            StatusCode = statusCode;
            ResponseContent = responseContent;
        }

        public APIStandardErrorResponse<object> GetLogContent()
        {
            if (!String.IsNullOrEmpty(ResponseContent))
            {
                return JsonSerializer.Deserialize<APIStandardErrorResponse<object>>(ResponseContent);
            }
            return null;
        }
    }
}
