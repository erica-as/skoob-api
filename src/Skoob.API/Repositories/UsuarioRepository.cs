using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;

namespace Skoob.API.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private static readonly List<Usuario> _usuarios = new();

    public IEnumerable<Usuario> ObterTodos() => _usuarios;

    public Usuario? ObterPorId(int id) => throw new NotImplementedException("Use ObterComEstante para o fluxo do Skoob.");

    public Usuario? ObterComEstante(int usuarioId)
    {
        return _usuarios.FirstOrDefault(u => u.Id == usuarioId);
    }

    public void Adicionar(Usuario entidade)
    {
        if (entidade.Id == 0)
            entidade.Id = _usuarios.Count + 1;
            
        _usuarios.Add(entidade);
    }

    public void Atualizar(Usuario entidade)
    {
        var index = _usuarios.FindIndex(u => u.Id == entidade.Id);
        if (index != -1)
        {
            _usuarios[index] = entidade;
        }
    }
}