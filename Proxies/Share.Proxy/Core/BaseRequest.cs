
using System.Text.Json;

namespace Share.Proxy.Core
{
    public class BaseRequest
    {
        public BaseRequest(string url, HttpMethod httpMethod)
        {
            Url = url;
            HttpMethod = httpMethod;
        }

        public string AccessToken { get; set; }
        public string Url { get; set; }
        public HttpMethod HttpMethod { get; set; }
        public virtual string PayloadString => string.Empty;
        public string UserAgent { get; set; }
        public string ApiKey { get; set; }
    }

    public class BaseRequest<T> : BaseRequest
    {
        public BaseRequest(string url, HttpMethod httpMethod, T payload) : base(url, httpMethod)
        {
            Payload = payload;
        }

        public override string PayloadString => JsonSerializer.Serialize(Payload);
        public T Payload { get; set; }


    }
}
