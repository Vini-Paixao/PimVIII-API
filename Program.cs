
using Microsoft.EntityFrameworkCore;
using PimVIII.Api.Data;
using PimVIII.Api.Interfaces;
using PimVIII.Api.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Recuperar a Connection String do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Adicionar o DbContext ao contêiner de serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString) // Informamos que vamos usar Npgsql (PostgreSQL)
);

// Adiciona os repositórios ao contêiner de serviços
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICriadorRepository, CriadorRepository>();
builder.Services.AddScoped<IConteudoRepository, ConteudoRepository>();

// Adiciona os controladores ao contêiner de serviços
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Esta opção ignora os loops infinitos durante a serialização
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // O Swagger é um requisito

var app = builder.Build();

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

