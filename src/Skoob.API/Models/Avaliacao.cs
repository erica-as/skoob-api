namespace Skoob.API.Models;

public class Avaliacao
{
    public int Id { get; set; }
    public int EstanteLivroId { get; set; }
    public int Nota { get; set; }
    public string Resenha { get; set; } = string.Empty;
    public DateTime DataAvaliacao { get; set; } = DateTime.UtcNow;
}