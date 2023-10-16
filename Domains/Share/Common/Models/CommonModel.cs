
namespace Common.Models
{
    public class PaginationModel<T>
    {
        public int TotalRecords { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public ICollection<T> Items { get; set; }
    }
}
