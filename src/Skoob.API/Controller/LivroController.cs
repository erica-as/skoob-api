using Microsoft.AspNetCore.Mvc;
using Skoob.API.DTOs;
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
    [EndpointSummary("Lista todos os livros cadastrados no catálogo global")]
    [EndpointDescription("Recupera a coleção completa de livros cadastrados no sistema, exibindo título, autor e total de páginas de cada obra.")]
    [ProducesResponseType(typeof(IEnumerable<Livro>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos()
    {
        var livros = await _service.BuscarLivros();
        return Ok(livros);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Busca um livro específico pelo ID")]
    [EndpointDescription("Recupera os detalhes detalhados de um único livro presente no catálogo através do identificador numérico fornecido na rota.")]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var livro = await _service.BuscarLivrosIdc(id);
        
        if (livro == null)
            return NotFound(new { mensagem = "Livro não encontrado." });

        return Ok(livro);
    }

    [HttpPost]
    [EndpointSummary("Cadastra um novo livro no catálogo")]
    [EndpointDescription("Adiciona uma nova obra literária ao catálogo global do sistema. Após a criação, o livro se torna disponível para que qualquer usuário o adicione à sua estante pessoal.")]
    [ProducesResponseType(typeof(Livro), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarLivroDto dto)
    {
        var livro = new Livro
        {
            Titulo = dto.Titulo,
            Autor = dto.Autor,
            TotalPaginas = dto.TotalPaginas
        };
        
        var sucesso = await _service.CriarLivros(livro);
        
        if (!sucesso)
            return BadRequest(new { mensagem = "Erro ao criar o livro." });

        return CreatedAtAction(nameof(ObterPorId), new { id = livro.Id }, livro);
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Atualiza as informações de um livro existente")]
    [EndpointDescription("Substitui os dados cadastrais (título, autor, páginas) de um livro específico no catálogo com base no ID da rota.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Livro livro)
    {
        var sucesso = await _service.AtualizarLivros(id, livro);
        
        if (!sucesso)
            return NotFound(new { mensagem = "Livro não encontrado ou erro ao atualizar." });

        return NoContent(); 
    }

    [HttpDelete("{id:int}")]
    [EndpointSummary("Remove um livro do catálogo global")]
    [EndpointDescription("Exclui permanentemente o registro de um livro do banco de dados por meio do ID informado.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(int id)
    {
        var sucesso = await _service.DeletarLivros(id);
        
        if (!sucesso)
            return NotFound(new { mensagem = "Livro não encontrado ou erro ao deletar." });

        return NoContent(); // Status 204
    }
}