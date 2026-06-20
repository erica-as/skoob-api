using Skoob.API.Models;

namespace Skoob.API.Services.Interfaces;

public interface IEstanteService
{
    void MudarStatusLivro(int usuarioId, int livroId, StatusLeitura novoStatus);
    void AdicionarResenha(int usuarioId, int livroId, int nota, string texto);
    
    void AdicionarLivroEstante(int usuarioId, int livroId, StatusLeitura status); 
    IEnumerable<EstanteLivro> ObterLivrosDaEstante(int usuarioId);
}