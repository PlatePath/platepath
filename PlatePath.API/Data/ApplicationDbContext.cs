using Microsoft.EntityFrameworkCore;

namespace PlatePath.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
    }
}