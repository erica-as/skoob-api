using Skoob.API.Repositories;
using Skoob.API.Repositories.Interfaces;
using Skoob.API.Services;
using Skoob.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Skoob.API.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Serviços Base da API
builder.Services.AddControllers();

// Mantém o gerador do arquivo openapi.json do próprio .NET
builder.Services.AddOpenApi(); 

// 2. Configuração do Banco de Dados
builder.Services.AddDbContext<SkoobDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Registrar Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

// 4. Registrar as Services
builder.Services.AddScoped<ILeituraService, LeituraService>();
builder.Services.AddScoped<IEstanteService, EstanteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILivroService, LivroService>();

var app = builder.Build();

// 5. Configuração do Pipeline de Requisições (Middlewares)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Skoob API v1");
        options.RoutePrefix = "swagger";    
    });
}

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();

app.Run();