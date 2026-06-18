using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;

namespace Skoob.API.Repositories.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Usuario? ObterComEstante(int usuarioId);
}