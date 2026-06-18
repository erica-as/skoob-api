namespace Skoob.API.Models;

public class EstanteLivro
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int LivroId { get; set; }
    public Livro? Livro { get; set; }
    public StatusLeitura Status { get; set; }
    public int PaginaAtual { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public List<HistoricoLeitura> Historicos { get; set; } = new();
    public Avaliacao? Avaliacao { get; set; }
}