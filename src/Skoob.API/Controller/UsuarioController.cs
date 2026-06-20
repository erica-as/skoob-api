using Microsoft.AspNetCore.Mvc;
using Skoob.API.DTOs;
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
    public IActionResult ObterTodos()
    {
        var usuarios = _usuarioService.ObterTodosUsuarios();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public IActionResult ObterPorId([FromRoute] int id) 
    {
        var usuario = _usuarioService.ObterUsuarioPorId(id);
    
        if (usuario == null)
            return NotFound(new { mensagem = "Usuário não encontrado." });

        return Ok(usuario);
    }
}