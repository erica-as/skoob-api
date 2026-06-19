using Skoob.API.Models;

namespace Skoob.API.Services.Interfaces;

public interface ILivroService
{
    Task<IEnumerable<Livro>> BuscarLivros();
    Task<Livro> BuscarLivrosIdc(int id);
    Task<bool> CriarLivros(Livro livro);
    Task<bool> AtualizarLivros(int id, Livro livro);
    Task<bool> DeletarLivros(int id);
}