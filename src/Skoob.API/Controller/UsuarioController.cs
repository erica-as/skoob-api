using Microsoft.AspNetCore.Mvc;
using Skoob.API.DTOs;
using Skoob.API.Models; 
using Skoob.API.Services.Interfaces;

namespace Skoob.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    [EndpointSummary("Cadastra um novo usuário no sistema")]
    [EndpointDescription("Cria um novo perfil de leitor validando a unicidade do e-mail informado. Configura a meta de leitura anual inicial.")]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Criar([FromBody] CriarUsuarioDto dto)
    {
        try
        {
            var usuario = _usuarioService.CriarUsuario(dto.Nome, dto.Email, dto.MetaLeituraAnual);
            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
    
    [HttpGet]
    [EndpointSummary("Lista todos os usuários cadastrados")]
    [EndpointDescription("Recupera a listagem global de todos os leitores que possuem conta ativa na plataforma.")]
    [ProducesResponseType(typeof(IEnumerable<Usuario>), StatusCodes.Status200OK)]
    public IActionResult ObterTodos()
    {
        var usuarios = _usuarioService.ObterTodosUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Busca um usuário específico pelo ID")]
    [EndpointDescription("Recupera os dados cadastrais de um único usuário e sua respectiva estante literária utilizando o identificador numérico da rota.")]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ObterPorId([FromRoute] int id) 
    {
        var usuario = _usuarioService.ObterUsuarioPorId(id);
    
        if (usuario == null)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }
}