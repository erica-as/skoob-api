using Microsoft.EntityFrameworkCore;
using Skoob.API.Data;
using Skoob.API.Repositories;
using Skoob.API.Services;
using Xunit;

namespace Skoob.Tests;

public class UsuarioServiceTests
{
    private SkoobDbContext CriarContextoEmMemoria()
    {
        var options = new DbContextOptionsBuilder<SkoobDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new SkoobDbContext(options);
    }

    [Fact]
    public void CriarUsuario_DeveAdicionarComSucesso_QuandoEmailForInedito()
    {
        using var contexto = CriarContextoEmMemoria();
        var repositorio = new UsuarioRepository(contexto);
        var servico = new UsuarioService(repositorio);

        string nome = "Carlos Silva";
        string email = "carlos@email.com";
        int meta = 12;

        var resultado = servico.CriarUsuario(nome, email, meta);

        Assert.NotNull(resultado);
        Assert.True(resultado.Id > 0);
        Assert.Equal(nome, resultado.Nome);
        Assert.Equal(email, resultado.Email);
        
        var usuarioNoBanco = contexto.Usuarios.FirstOrDefault(u => u.Email == email);
        Assert.NotNull(usuarioNoBanco);
    }

    [Fact]
    public void CriarUsuario_DeveLancasExcecao_QuandoEmailJaEstiverCadastrado()
    {
        using var contexto = CriarContextoEmMemoria();
        var repositorio = new UsuarioRepository(contexto);
        var servico = new UsuarioService(repositorio);

        string emailRepetido = "ana@email.com";
        servico.CriarUsuario("Ana Maria", emailRepetido, 5);

        var excecao = Assert.Throws<ArgumentException>(() => 
            servico.CriarUsuario("Ana Outra", emailRepetido, 10)
        );

        Assert.Equal("O e-mail informado já está cadastrado.", excecao.Message);
    }

    [Fact]
    public void ObterUsuarioPorId_DeveRetornarNulo_QuandoUsuarioNaoExistir()
    {
        using var contexto = CriarContextoEmMemoria();
        var repositorio = new UsuarioRepository(contexto);
        var servico = new UsuarioService(repositorio);

        var resultado = servico.ObterUsuarioPorId(999); // Id inexistente

        Assert.Null(resultado);
    }
}