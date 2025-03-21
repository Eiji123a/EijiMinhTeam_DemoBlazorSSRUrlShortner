using DemoBlazorSSRUrlShortner.Data;
using DemoBlazorSSRUrlShortner.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorSSRUrlShortner.Repository
{
    public interface IUrlRepository
    {
        Task<UrlMapping> GetUrlMappingByShortcodeAsync(string shortcode);
        Task<UrlMapping> GetUrlMappingByOriginalUrlAsync(string original);
        Task SaveMapping(UrlMapping mapping);

    }

    public class UrlRepository(AppDbcontext context) : IUrlRepository
    {
        public async Task<UrlMapping> GetUrlMappingByOriginalUrlAsync(string original) 
        {
            return await context.UrlMapping.FirstOrDefaultAsync(x => x.OriginalUrl  == original);
        }
        public async Task<UrlMapping> GetUrlMappingByShortcodeAsync(string shortcode) 
        {
            return await context.UrlMapping.FirstOrDefaultAsync(x => x.ShortCode == shortcode);
        }

        public async Task SaveMapping(UrlMapping mapping) 
        {
            context.UrlMapping.Add(mapping);
            await context.SaveChangesAsync();
            return;
        }
    }
}
