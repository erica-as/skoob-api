namespace Skoob.API.Models;

public class HistoricoLeitura
{
    public int Id { get; set; }
    public int EstanteLivroId { get; set; }
    public int PaginaLida { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public DateTime DataRegistro { get; set; } = DateTime.UtcNow;
}