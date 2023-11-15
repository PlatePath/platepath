using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatePath.API.Data.Models.Authentication;

namespace PlatePath.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder, _configuration);
        }

        public DbSet<Recipe> Recipes { get; set; }

        private static void SeedRoles(ModelBuilder builder, IConfiguration configuration)
        {
            var rolesSeedData = configuration.GetSection("RolesSeedData").Get<List<RoleSeedData>>();

            builder.Entity<IdentityRole>().HasData(rolesSeedData.Select(roleData =>
                new IdentityRole
                {
                    Name = roleData.Name,
                    ConcurrencyStamp = roleData.ConcurrencyStamp,
                    NormalizedName = roleData.NormalizedName
                }
            ));
        }
    }
}