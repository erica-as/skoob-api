using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;
using Skoob.API.Services.Interfaces; // Adicione o namespace

namespace Skoob.API.Services;

public class LeituraService : ILeituraService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ILivroRepository _livroRepository;

    public LeituraService(IUsuarioRepository usuarioRepository, ILivroRepository libroRepository)
    {
        _usuarioRepository = usuarioRepository;
        _livroRepository = libroRepository;
    }
    
    public void AtualizarProgresso(int usuarioId, int livroId, int novaPagina, string comentario)
    {
        var usuario = _usuarioRepository.ObterComEstante(usuarioId) 
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");

        var estanteItem = usuario.Estante.FirstOrDefault(e => e.LivroId == livroId)
                          ?? throw new KeyNotFoundException("Livro não encontrado na estante do usuário.");

        var livro = _livroRepository.ObterPorId(livroId)
                    ?? throw new KeyNotFoundException("Livro não cadastrado no sistema.");

        if (novaPagina < 0)
            throw new ArgumentException("A página atual não pode ser negativa.");

        if (novaPagina > livro.TotalPaginas)
            throw new ArgumentException("A página atual não pode ser maior que o total de páginas do livro.");

        estanteItem.PaginaAtual = novaPagina;
        estanteItem.Historicos.Add(new HistoricoLeitura 
        { 
            EstanteLivroId = estanteItem.Id, 
            PaginaLida = novaPagina, 
            Comentario = comentario 
        });

        if (novaPagina == livro.TotalPaginas)
        {
            estanteItem.Status = StatusLeitura.Lido;
            estanteItem.DataTermino = DateTime.UtcNow;
        }

        _usuarioRepository.Atualizar(usuario);
    }

    public double CalcularPorcentagemConclusao(Livro livro, int paginaAtual)
    {
        if (livro == null) throw new ArgumentNullException(nameof(livro));
        if (paginaAtual < 0 || paginaAtual > livro.TotalPaginas) 
            throw new ArgumentException("Página atual inválida.");

        if (livro.TotalPaginas == 0) return 0;

        return Math.Round(((double)paginaAtual / livro.TotalPaginas) * 100, 2);
    }

    public bool VerificarSeMetaAnualFoiAtingida(int usuarioId)
    {
        var usuario = _usuarioRepository.ObterComEstante(usuarioId) 
                      ?? throw new KeyNotFoundException("Usuário não encontrado.");

        int anoAtual = DateTime.UtcNow.Year;
        
        int livrosLidosNoAno = usuario.Estante.Count(e => 
            e.Status == StatusLeitura.Lido && 
            e.DataTermino.HasValue && 
            e.DataTermino.Value.Year == anoAtual);

        return livrosLidosNoAno >= usuario.MetaLeituraAnual;
    }
}