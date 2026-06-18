namespace Skoob.API.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int MetaLeituraAnual { get; set; }
    public List<EstanteLivro> Estante { get; set; } = new();
}