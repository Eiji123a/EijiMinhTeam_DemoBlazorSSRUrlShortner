namespace DemoBlazorSSRUrlShortner.UrlHelpers
{
    public interface IUrlHelper
    {
        string ConstructUrl(string shortcode);
        string GenerateShortcode();
        bool IsValidUrl(string url);
    }
}
