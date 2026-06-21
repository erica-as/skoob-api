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
    [EndpointSummary("Adiciona um livro à estante pessoal do usuário")]
    [EndpointDescription("Associa um livro previamente cadastrado no catálogo à estante de um usuário específico informando o status inicial de leitura.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [EndpointSummary("Altera o status de leitura de um livro")]
    [EndpointDescription("Atualiza a situação de um livro na estante (ex: Lendo, Quero Ler, Lido). Caso o novo status seja 'Lido', a página atual é automaticamente sincronizada com o total de páginas do livro.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [EndpointSummary("Adiciona uma nota e resenha para um livro lido")]
    [EndpointDescription("Permite que o usuário atribua uma nota estritamente entre 1 e 5 e escreva um texto descritivo (resenha) sobre a obra avaliada.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [EndpointSummary("Lista todos os livros contidos na estante do usuário")]
    [EndpointDescription("Recupera a coleção de livros associados ao usuário, trazendo detalhes individuais como paginação atual, status de leitura e avaliações aplicadas.")]
    [ProducesResponseType(typeof(IEnumerable<EstanteLivro>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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