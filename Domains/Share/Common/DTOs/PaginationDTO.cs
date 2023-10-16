
using System.Text.Json.Serialization;

namespace Common.DTOs
{
    public interface IPaginationDTO<T>
    {
        [JsonPropertyName("total_records")]
        int TotalRecords { get; set; }

        [JsonPropertyName("page_no")]
        int PageNo { get; set; }

        [JsonPropertyName("page_size")]
        int PageSize { get; set; }

        [JsonPropertyName("total_pages")]
        int TotalPages { get; set; }

        [JsonPropertyName("has_previous_page")]
        bool HasPreviousPage { get; set; }

        [JsonPropertyName("has_next_page")]
        bool HasNextPage { get; set; }

        [JsonPropertyName("items")]
        ICollection<T> Items { get; set; }
    }

    public class PaginationDTO<T> : IPaginationDTO<T>
    {
        [JsonPropertyName("total_records")]
        public int TotalRecords { get; set; }

        [JsonPropertyName("page_no")]
        public int PageNo { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("has_previous_page")]
        public bool HasPreviousPage { get; set; }

        [JsonPropertyName("has_next_page")]
        public bool HasNextPage { get; set; }

        [JsonPropertyName("items")]
        public virtual ICollection<T> Items { get; set; } = new List<T>();

    }
}
