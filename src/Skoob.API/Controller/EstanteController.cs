using Microsoft.AspNetCore.Mvc;
using Skoob.API.DTOs;
using Skoob.API.Models;
using Skoob.API.Services.Interfaces; 

namespace Skoob.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstanteController : ControllerBase
{
    private readonly IEstanteService _estanteService;

    public EstanteController(IEstanteService estanteService)
    {
        _estanteService = estanteService;
    }

    [HttpPost("{usuarioId:int}/livros")]
    public IActionResult AdicionarLivro([FromRoute] int usuarioId, [FromBody] AdicionarLivroDto dto)
    {
        try
        {
            _estanteService.AdicionarLivroEstante(usuarioId, dto.LivroId, dto.Status);
            return Ok(new { mensagem = "Livro adicionado à estante com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); 
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message); 
        }
    }

    [HttpPut("{usuarioId:int}/livros/{livroId:int}/status")]
    public IActionResult AlterarStatus([FromRoute] int usuarioId, [FromRoute] int livroId, [FromQuery] StatusLeitura novoStatus)
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

    [HttpPost("{usuarioId:int}/livros/{livroId:int}/avaliar")]
    public IActionResult AvaliarLivro([FromRoute] int usuarioId, [FromRoute] int livroId, [FromQuery] int nota, [FromBody] string resenha)
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
    
    [HttpGet("{usuarioId:int}/livros")]
    public IActionResult ObterLivrosEstante([FromRoute] int usuarioId)
    {
        try
        {
            var livrosEstante = _estanteService.ObterLivrosDaEstante(usuarioId);
            return Ok(livrosEstante);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message }); 
        }
    }
}