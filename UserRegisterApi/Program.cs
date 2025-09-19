using MongoDB.Driver;
using UserRegisterApi.Models;
using UserRegisterApi.Services;

var builder = WebApplication.CreateBuilder(args);

// bind settings
var mongoSection = builder.Configuration.GetSection("MongoSettings");
var mongoSettings = mongoSection.Get<MongoSettings>() ?? new MongoSettings();
builder.Services.Configure<MongoSettings>(mongoSection);

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(mongoSettings.ConnectionString));
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var db = client.GetDatabase(mongoSettings.DatabaseName);
    return db.GetCollection<User>(mongoSettings.UsersCollectionName);
});
builder.Services.AddSingleton<UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
    var existingUsers = await userService.GetAllAsync();
    if (existingUsers.Count == 0)
    {
        await userService.CreateAsync(new User
        {
            Nome = "Bernardo Souza Alvim",
            Email = "bernardo.alvim@sga.pucminas.br",
            Senha = "alvim123",
            CodigoPessoa = "859148",
            LembreteSenha = "Nome da minha antiga gata",
            Idade = 19,
            Sexo = "M"
        });
    }
}

app.Run();