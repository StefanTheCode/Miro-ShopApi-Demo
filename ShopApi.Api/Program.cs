using ShopApi.Infrastructure;
using ShopApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Clean Architecture wiring: Api depends on Application + Infrastructure
var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? "Data Source=shop.db";
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Create + seed the SQLite database on startup so the demo runs immediately
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
