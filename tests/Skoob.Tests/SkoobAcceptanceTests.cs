using Microsoft.EntityFrameworkCore;
using Skoob.API.Data;
using Skoob.API.Models;
using Skoob.API.Repositories;
using Skoob.API.Services;

namespace Skoob.Tests;

public class SkoobAcceptanceTests
{
    private SkoobDbContext CriarDbContextContextoEmMemoria()
    {
        var options = new DbContextOptionsBuilder<SkoobDbContext>()
            .UseInMemoryDatabase(databaseName: "Skoob_Acceptance_DB_" + Guid.NewGuid().ToString())
            .Options;

        return new SkoobDbContext(options);
    }

    [Fact]
    public void JornadaDoUsuario_CriarContaEAdicionarLivroNaEstante_DeveFuncionarComSucesso()
    {
        using var context = CriarDbContextContextoEmMemoria();
        
        var usuarioRepository = new UsuarioRepository(context);
        var usuarioService = new UsuarioService(usuarioRepository);
        
        var livroRepository = new LivroRepository(); 
        var estanteService = new EstanteService(usuarioRepository, livroRepository);

        var usuario = usuarioService.CriarUsuario("Juliana Ramos", "juliana@teste.com", 4);
        Assert.NotNull(usuario);

        var livro = new Livro { Id = 99, Titulo = "Dom Casmurro", Autor = "Machado de Assis", TotalPaginas = 256 };
        livroRepository.Adicionar(livro); 

        estanteService.AdicionarLivroEstante(usuario.Id, livro.Id, StatusLeitura.QueroLer);

        var livrosNaEstante = estanteService.ObterLivrosDaEstante(usuario.Id);
        
        Assert.NotNull(livrosNaEstante);
        Assert.Single(livrosNaEstante);
        
        var primeiroLivro = System.Linq.Enumerable.First(livrosNaEstante);
        Assert.Equal(livro.Id, primeiroLivro.LivroId);
        Assert.Equal(StatusLeitura.QueroLer, primeiroLivro.Status);
    }
}