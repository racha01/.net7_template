
namespace Common.AppSettings.TestAPI
{
    public class AppSettings
    {
        public Pagination Pagination { get; set; }
        public AuthorizationLineService AuthorizationLineService { get; set; }
    }

    public class Pagination
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class AuthorizationLineService
    {
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
        public string OAuthUrl { get; set; } = default!;
        public string GrantType { get; set; } = default!;
    }
}
