using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;

namespace Skoob.API.Repositories;

public class LivroRepository : ILivroRepository
{
    private static readonly List<Livro> _livros = new();

    public IEnumerable<Livro> ObterTodos() => _livros;

    public Livro? ObterPorId(int id) => _livros.FirstOrDefault(l => l.Id == id);
    
    public void Adicionar(Livro entidade)
    {
        if (entidade.Id == 0)
            entidade.Id = _livros.Count + 1;
            
        _livros.Add(entidade);
    }

    public void Atualizar(Livro entidade)
    {
        var index = _livros.FindIndex(l => l.Id == entidade.Id);
        if (index != -1)
        {
            _livros[index] = entidade;
        }
    }
}