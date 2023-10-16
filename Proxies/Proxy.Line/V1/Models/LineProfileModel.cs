
using System.Text.Json.Serialization;

namespace Proxy.Line.V1.Models
{
    public class LineProfileModel
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("pictureUrl")]
        public string PictureUrl { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}
