using DemoBlazorSSRUrlShortner.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoBlazorSSRUrlShortner.Data
{
    public class AppDbcontext(DbContextOptions<AppDbcontext> options) : DbContext (options)
    {
        public DbSet<UrlMapping> UrlMapping => Set<UrlMapping>();
    }
}
