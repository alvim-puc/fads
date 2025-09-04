using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ProductService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
    
    if (!dbContext.Products.Any())
    {

        dbContext.Products.AddRange(
            new Product
            {
                Nome = "Bernardo Souza Alvim",
                Codigo = "859148",
                Descricao = "Engenheiro de Software Fullstack",
                Categoria = "Desenvolvedores",
                Preco = 2000000000m,
                QuantidadeEmEstoque = 1,
                Avaliacao = 4.9f
            }
        );
        dbContext.SaveChanges();
    }
}

app.Run();