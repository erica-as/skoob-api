using Skoob.API.Models;

namespace Skoob.API.Repositories.Interfaces;

public interface ILivroRepository : IRepository<Livro>
{
    Livro? ObterPorIsbn(string isbn);
}