using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;
using Skoob.API.Services.Interfaces;

namespace Skoob.API.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public Usuario CriarUsuario(string nome, string email, int metaLeitura)
    {
        var usuarioExistente = _usuarioRepository.ObterPorEmail(email);
        if (usuarioExistente != null)
        {
            throw new ArgumentException("O e-mail informado já está cadastrado.");
        }

        var novoUsuario = new Usuario
        {
            Nome = nome,
            Email = email,
            MetaLeituraAnual = metaLeitura
        };

        _usuarioRepository.Adicionar(novoUsuario);
        return novoUsuario;
    }

    public Usuario? ObterUsuarioPorId(int id)
    {
        return _usuarioRepository.ObterComEstante(id);
    }

    public IEnumerable<Usuario> ObterTodosUsuarios() 
    {
        return _usuarioRepository.ObterTodos();
    }
}