using System.ComponentModel.DataAnnotations;
using Skoob.API.Models;

namespace Skoob.API.DTOs;

public class AdicionarLivroDto
{
    [Required]
    public int LivroId { get; set; }
    
    [Required]
    public string Titulo { get; set; }

    [Required]
    public StatusLeitura Status { get; set; }
}