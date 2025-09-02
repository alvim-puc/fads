using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    
    // Seed initial data
    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(
            new Product
            {
                Name = "Notebook Gamer",
                Code = "NTB-GMR-001",
                Price = 3500.00m,
                Description = "Notebook para jogos de alta performance",
                StockQuantity = 10,
                Rating = 4.5m,
                Category = "Eletrônicos"
            },
            new Product
            {
                Name = "Smartphone Premium",
                Code = "123456", // Seu código real aqui
                Price = 2500.00m,
                Description = "Smartphone flagship com todas as funcionalidades",
                StockQuantity = 15,
                Rating = 4.8m,
                Category = "Eletrônicos"
            }
        );
        dbContext.SaveChanges();
    }
}

app.Run();