using Microsoft.AspNetCore.Mvc;
using Skoob.API.Models;
using Skoob.API.Services.Interfaces; 

namespace Skoob.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _service;

    public LivroController(ILivroService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var livros = await _service.BuscarLivros();
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var livro = await _service.BuscarLivrosIdc(id);
        
        if (livro == null)
            return NotFound(new { mensagem = "Livro não encontrado." });

        return Ok(livro);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Livro livro)
    {
        var sucesso = await _service.CriarLivros(livro);
        
        if (!sucesso)
            return BadRequest(new { mensagem = "Erro ao criar o livro." });

        return CreatedAtAction(nameof(ObterPorId), new { id = livro.Id }, livro);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Livro livro)
    {
        var sucesso = await _service.AtualizarLivros(id, livro);
        
        if (!sucesso)
            return NotFound(new { mensagem = "Livro não encontrado ou erro ao atualizar." });

        return NoContent(); // Status 204 (Sucesso, sem conteúdo no corpo)
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var sucesso = await _service.DeletarLivros(id);
        
        if (!sucesso)
            return NotFound(new { mensagem = "Livro não encontrado ou erro ao deletar." });

        return NoContent(); // Status 204
    }
}