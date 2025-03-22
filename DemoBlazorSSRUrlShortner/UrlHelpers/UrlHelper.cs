namespace DemoBlazorSSRUrlShortner.UrlHelpers
{
    public class UrlHelper(IHttpContextAccessor httpContextAccessor) : IUrlHelper
    {
        public string ConstructUrl(string shortcode)
        {
            var request = httpContextAccessor.HttpContext?.Request;
            var baseUrl = $"{request!.Scheme}://{request.Host}";
            return $"{baseUrl}/{shortcode}";
        }

        public string GenerateShortcode()
        {
            return Guid.NewGuid().ToString("n")[..8];
        }

        public bool IsValidUrl(string url)
        {
            if(string.IsNullOrEmpty(url)) return false;

            return Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}
