using Skoob.API.Models;

namespace Skoob.API.Services.Interfaces;

public interface IUsuarioService
{
    Usuario CriarUsuario(string nome, string email, int metaLeitura);
    Usuario? ObterUsuarioPorId(int id);
}