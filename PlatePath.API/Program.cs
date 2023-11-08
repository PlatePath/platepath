using Microsoft.EntityFrameworkCore;
using PlatePath.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


bool isDevelopment = builder.Environment.IsDevelopment();

// Get the appropriate connection string
var connectionStringName = isDevelopment ? "PlatePathDb" : "PlatePathProdDb";
var connectionString = builder.Configuration.GetConnectionString(connectionStringName);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Apply migrations if any.
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
