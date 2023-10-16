
using Common.Model;
using System.Text.Json;

namespace Share.Proxy.Exceptions
{
    public class ServiceFailedException : Exception
    {
        public APIStandardErrorResponse Response { get; private set; }

        public ServiceFailedException(int statusCode, string content)
            : base($"There is error when call service: {statusCode} - {content}")
        {
            StatusCode = statusCode;

            if (!string.IsNullOrEmpty(content))
            {
                Response = JsonSerializer.Deserialize<APIStandardErrorResponse>(content);

                if (Response.error_info != null)
                {
                    ErrorMessageEn = Response.error_info.message_en;
                    ErrorMessageTh = Response.error_info.message_th;

                    if (Response.error_info.display != null)
                    {
                        Display = Response.error_info.display[APIErrorStandardDeviceEnum.web.ToString()];
                    }
                }
                else
                {
                    //legacy platform
                    // Deserialize the JSON string
                    var jsonObject = JsonSerializer.Deserialize<Dictionary<string, string>>(content);

                    // Get the value of the "message" attribute
                    if (jsonObject.TryGetValue("message", out string message))
                    {
                        ErrorMessageEn = message;
                        ErrorMessageTh = message;
                        Display = APIErrorStandardDeviceEnum.web.ToString();
                    }
                }
            }
        }

        public int StatusCode { get; private set; }
        public string ErrorMessageEn { get; private set; }
        public string ErrorMessageTh { get; private set; }
        public string Display { get; private set; }
    }
}
