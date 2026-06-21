using Microsoft.EntityFrameworkCore;
using Skoob.API.Data;
using Skoob.API.Repositories;
using Skoob.API.Services;
using Xunit;

namespace Skoob.Tests;

public class UsuarioIntegrationTests
{
    private SkoobDbContext CriarDbContextContextoEmMemoria()
    {
        var options = new DbContextOptionsBuilder<SkoobDbContext>()
            .UseInMemoryDatabase(databaseName: "Skoob_Integration_DB_" + Guid.NewGuid().ToString())
            .Options;

        return new SkoobDbContext(options);
    }

    [Fact]
    public void SalvarUsuarioNoBanco_DevePersistirEGerarIdCorretamente()
    {
        using var context = CriarDbContextContextoEmMemoria();
        
        var usuarioRepository = new UsuarioRepository(context);
        var usuarioService = new UsuarioService(usuarioRepository);
        
        var usuarioSalvo = usuarioService.CriarUsuario("Lucas Nogueira", "lucas@teste.com", 6);

        var usuarioNoBanco = context.Usuarios.FirstOrDefault(u => u.Email == "lucas@teste.com");
        Assert.NotNull(usuarioNoBanco);
        Assert.True(usuarioNoBanco.Id > 0);
        Assert.Equal("Lucas Nogueira", usuarioNoBanco.Nome);
    }
}