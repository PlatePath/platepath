using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlatePath.Data;

var builder = WebApplication.CreateBuilder(args);

// Determine the environment
bool isDevelopment = builder.Environment.IsDevelopment();

// Get the appropriate connection string
var connectionStringName = isDevelopment ? "PlatePathDb" : "PlatePathProdDb";
var connectionString = builder.Configuration.GetConnectionString(connectionStringName);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Apply migrations if any.
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
