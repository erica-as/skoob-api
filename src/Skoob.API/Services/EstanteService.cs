using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;
using Skoob.API.Services.Interfaces;

namespace Skoob.API.Services;

public class EstanteService : IEstanteService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILivroRepository _livroRepository;

    public EstanteService(IUsuarioRepository usuarioRepository, ILivroRepository livroRepository)
    {
        _usuarioRepository = usuarioRepository;
        _livroRepository = livroRepository;
    }
    
    public void AdicionarLivroEstante(int usuarioId, int livroId, StatusLeitura status)
    {
        var usuario = _usuarioRepository.ObterComEstante(usuarioId) 
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");

        if (usuario.Estante.Any(e => e.LivroId == livroId))
            throw new ArgumentException("Livro já está na estante.");

        var novoItem = new EstanteLivro
        {
            UsuarioId = usuarioId,
            LivroId = livroId,
            Status = status,
            PaginaAtual = 0
        };

        usuario.Estante.Add(novoItem);
        _usuarioRepository.Atualizar(usuario);
    }

    public void MudarStatusLivro(int usuarioId, int livroId, StatusLeitura novoStatus)
    {
        var usuario = _usuarioRepository.ObterComEstante(usuarioId) 
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");

        var estanteItem = usuario.Estante.FirstOrDefault(e => e.LivroId == livroId)
                          ?? throw new KeyNotFoundException("Livro não está na estante.");

        var livro = _livroRepository.ObterPorId(livroId);

        estanteItem.Status = novoStatus;

        if (novoStatus == StatusLeitura.Lido && livro != null)
        {
            estanteItem.PaginaAtual = livro.TotalPaginas;
        }

        _usuarioRepository.Atualizar(usuario);
    }

    public void AdicionarResenha(int usuarioId, int livroId, int nota, string texto)
    {
        if (nota < 1 || nota > 5)
            throw new ArgumentOutOfRangeException(nameof(nota), "A nota deve estar estritamente entre 1 e 5.");

        var usuario = _usuarioRepository.ObterComEstante(usuarioId) 
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");

        var estanteItem = usuario.Estante.FirstOrDefault(e => e.LivroId == livroId)
                          ?? throw new KeyNotFoundException("O livro precisa estar na estante para ser avaliado.");

        estanteItem.Avaliacao = new Avaliacao
        {
            EstanteLivroId = estanteItem.Id,
            Nota = nota,
            Resenha = texto
        };

        _usuarioRepository.Atualizar(usuario);
    }
    
    public IEnumerable<EstanteLivro> ObterLivrosDaEstante(int usuarioId)
    {
        var usuario = _usuarioRepository.ObterComEstante(usuarioId)
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");

        return usuario.Estante;
    }
}