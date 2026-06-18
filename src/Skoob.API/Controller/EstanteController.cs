using Microsoft.AspNetCore.Mvc;
using Skoob.API.DTOs;
using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;
using Skoob.API.Services.Interfaces; 

namespace Skoob.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstanteController : ControllerBase
{
    // Depende da Interface agora
    private readonly IEstanteService _estanteService;
    private readonly IUsuarioRepository _usuarioRepository;

    public EstanteController(IEstanteService estanteService, IUsuarioRepository usuarioRepository)
    {
        _estanteService = estanteService;
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost("{usuarioId}/livros")]
    public IActionResult AdicionarLivro(int usuarioId, [FromBody] AdicionarLivroDto dto)
    {
        var usuario = _usuarioRepository.ObterComEstante(usuarioId);
        if (usuario == null) return NotFound("Usuário não encontrado.");

        if (usuario.Estante.Any(e => e.LivroId == dto.LivroId))
            return BadRequest("Livro já está na estante.");

        var novoItem = new EstanteLivro
        {
            Id = usuario.Estante.Count + 1,
            UsuarioId = usuarioId,
            LivroId = dto.LivroId,
            Status = dto.Status,
            PaginaAtual = 0
        };

        usuario.Estante.Add(novoItem);
        _usuarioRepository.Atualizar(usuario);

        return Ok(novoItem);
    }

    [HttpPut("{usuarioId}/livros/{livroId}/status")]
    public IActionResult AlterarStatus(int usuarioId, int livroId, [FromQuery] StatusLeitura novoStatus)
    {
        try
        {
            _estanteService.MudarStatusLivro(usuarioId, livroId, novoStatus);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{usuarioId}/livros/{livroId}/avaliar")]
    public IActionResult AvaliarLivro(int usuarioId, int livroId, [FromQuery] int nota, [FromBody] string resenha)
    {
        try
        {
            _estanteService.AdicionarResenha(usuarioId, livroId, nota, resenha);
            return Ok("Avaliação salva com sucesso.");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}