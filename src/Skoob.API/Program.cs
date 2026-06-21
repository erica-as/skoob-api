using Skoob.API.Repositories;
using Skoob.API.Repositories.Interfaces;
using Skoob.API.Services;
using Skoob.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Skoob.API.Data;

// Inicializa o construtor da aplicação web carregando as configurações padrão do sistema (como appsettings.json, variáveis de ambiente e logs)
var builder = WebApplication.CreateBuilder(args);

// Adiciona o suporte aos Controllers da arquitetura MVC (essencial para expor endpoints HTTP que recebem DTOs e retornam JSON)
builder.Services.AddControllers();

// Configuração de Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Permite requisições vindas de qualquer endereço de origem (URL)
            .AllowAnyMethod()   // Permite qualquer método HTTP (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader();  // Permite qualquer cabeçalho HTTP customizado enviado pelo cliente
    });
});

// Mantém o gerador do arquivo openapi.json do próprio .NET
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Servers.Clear(); 
        return Task.CompletedTask;
    });
});

// Configuração do Banco de Dados
builder.Services.AddDbContext<SkoobDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Obtém a string de conexão salva no appsettings.json

// Registrar Repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();

// Registrar as Services
builder.Services.AddScoped<IEstanteService, EstanteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILivroService, LivroService>();

// Compila as configurações descritas e monta formalmente o pipeline de execução da aplicação web
var app = builder.Build();

// Força o redirecionamento automático de requisições HTTP normais para o protocolo seguro HTTPS (Garante integridade e criptografia)
app.UseHttpsRedirection();

// Ativa a política de CORS "AllowAll" que foi configurada, filtrando as requisições de origens cruzadas
app.UseCors("AllowAll");

// Mapeamento da documentação
// Expõe a rota que disponibiliza o arquivo de especificação técnica openapi.json (geralmente sob a rota /openapi/v1.json)
app.MapOpenApi();

// Configura o middleware de interface gráfica do Swagger UI para ler o arquivo openapi.json e montar a tela de testes visuais dos endpoints
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Skoob API v1"); 
    options.RoutePrefix = "swagger";    
});

// Executa a validação de segurança e middlewares de autorização do ASP.NET Core (permite proteger endpoints com papéis de acesso)
app.UseAuthorization(); 

// Varre as classes anotadas com [ApiController] e vincula as rotas declaradas nelas (ex: [Route("api/[controller]")]) ao pipeline HTTP da aplicação
app.MapControllers();

// Executa as migrations automaticamente ao iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SkoobDbContext>();
        
        context.Database.Migrate(); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao aplicar as migrations no banco de dados.");
    }
}

// Inicializa os escutadores HTTP e liga os servidores da aplicação, mantendo o processo rodando ativamente e aguardando chamadas
app.Run();