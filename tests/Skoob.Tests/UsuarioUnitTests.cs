using Skoob.API.Models;

namespace Skoob.Tests;

public class UsuarioUnitTests
{
    [Fact]
    public void CriarUsuario_ComDadosValidos_DeveDefinirPropriedadesCorretamente()
    {
        var nomeEsperado = "Érica Silva";
        var emailEsperado = "erica@teste.com";
        var metaEsperada = 12;

        var usuario = new Usuario
        {
            Nome = nomeEsperado,
            Email = emailEsperado,
            MetaLeituraAnual = metaEsperada
        };

        Assert.Equal(nomeEsperado, usuario.Nome);
        Assert.Equal(emailEsperado, usuario.Email);
        Assert.Equal(metaEsperada, usuario.MetaLeituraAnual);
    }
}