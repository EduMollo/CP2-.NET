using LojaVirtual.Infrastructure.Persistance;
using LojaVirtual.Infrastructure.Persistance.Repositories;
using LojaVirtual.Application.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar DbContext com MySQL
builder.Services.AddDbContext<LojaVirtualContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não found.");
    
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Registrar Repositórios
builder.Services.AddScoped<ILojaRepository, LojaRepository>();
builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Health Check endpoint
app.MapGet("/", () => new { message = "Loja Virtual API está funcionando!" })
    .WithName("HealthCheck")
    .WithOpenApi();

app.MapControllers();

app.Run();
