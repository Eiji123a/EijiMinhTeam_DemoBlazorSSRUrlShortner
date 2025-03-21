using DemoBlazorSSRUrlShortner.Models;
using DemoBlazorSSRUrlShortner.Repository;
using DemoBlazorSSRUrlShortner.UrlHelpers;

namespace DemoBlazorSSRUrlShortner.Services
{
    public class UrlService(IUrlRepository urlRepository, IUrlHelper urlHelper) : IUrlService
    {
        public async Task<string?> GetOriginalUrlAsync(string shortUrl)
        {
            var check = await urlRepository.GetUrlMappingByShortcodeAsync(shortUrl);
            return check == null ? null! : check.OriginalUrl;
        }

        public async Task<string?> ShortenUrlAsync(string originalUrl)
        {
            bool validateUrl = urlHelper.IsValidUrl(originalUrl);
            if(!validateUrl)
                return null!;

            var checkMap = await urlRepository.GetUrlMappingByOriginalUrlAsync(originalUrl);
            if(checkMap == null)
            {
                var shortcode = urlHelper.GenerateShortcode();
                var mapping = new UrlMapping
                {
                    OriginalUrl = originalUrl,
                    ShortCode = shortcode
                };
                await urlRepository.SaveMapping(mapping);
                return urlHelper.ConstructUrl(shortcode);
            }
            return urlHelper.ConstructUrl(checkMap.ShortCode);
        }
    }
}
