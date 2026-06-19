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
        var usuario = _usuarioService.CriarUsuario(dto.Nome, dto.Email, dto.MetaLeituraAnual);
        return Ok(usuario); 
    }
}