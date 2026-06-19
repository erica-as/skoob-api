namespace Skoob.API.DTOs;

public class CriarUsuarioDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int MetaLeituraAnual { get; set; }
}