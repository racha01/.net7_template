
namespace Common.AppSettings.TestAPI
{
    public class AppSettings
    {
        public Pagination Pagination { get; set; }
    }

    public class Pagination
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
