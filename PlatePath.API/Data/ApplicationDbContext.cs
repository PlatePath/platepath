using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlatePath.API.Data.Models.ActivityLevels;
using PlatePath.API.Data.Models.Authentication;
using PlatePath.API.Data.Models.Genders;
using PlatePath.API.Data.Models.MealPlans;
using PlatePath.API.Data.Models.Recipes;
using PlatePath.API.Data.Models.Users;
using PlatePath.API.Data.Models.WeightGoals;
using PlatePath.API.Enums;

namespace PlatePath.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
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
            SeedGenders(builder);
            SeedActivityLevels(builder);
            SeedWeightGoals(builder);
        }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<DietLabel> DietLabels { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<MealPlan> MealPlans { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<ActivityLevel> ActivityLevels { get; set; }

        public DbSet<ActivityLevel> WeightGoals { get; set; }

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

        private static void SeedGenders(ModelBuilder builder)
        {
            builder.Entity<Gender>().HasData(Enum.GetValues(typeof(GenderEnum))
                        .Cast<GenderEnum>()
                        .Select(e => new Gender
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                        .ToList());
        }

        private static void SeedActivityLevels(ModelBuilder builder)
        {
            builder.Entity<ActivityLevel>().HasData(Enum.GetValues(typeof(ActivityLevelEnum))
                        .Cast<ActivityLevelEnum>()
                        .Select(e => new ActivityLevel
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                        .ToList());
        }

        private static void SeedWeightGoals(ModelBuilder builder)
        {
            builder.Entity<WeightGoal>().HasData(Enum.GetValues(typeof(WeightGoalEnum))
                        .Cast<WeightGoalEnum>()
                        .Select(e => new WeightGoal
                        {
                            Id = (int)e,
                            Name = e.ToString()
                        })
                        .ToList());
        }


    }
}