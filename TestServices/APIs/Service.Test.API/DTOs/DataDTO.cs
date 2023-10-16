using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Service.Test.API.DTOs
{
    public class DataDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }
    }

    public class CreateDataDTO
    {
        [MaxLength(100)]
        [JsonPropertyName("data")]
        public string Data { get; set; }
    }

    public class ResponseCreateDataDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
