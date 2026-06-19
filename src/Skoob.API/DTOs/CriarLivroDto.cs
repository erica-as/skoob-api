namespace Skoob.API.DTOs;

public class CriarLivroDto
{
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int TotalPaginas { get; set; }
}