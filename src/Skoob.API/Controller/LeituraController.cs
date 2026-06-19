using Microsoft.AspNetCore.Mvc;
using Skoob.API.Services.Interfaces; 

namespace Skoob.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeituraController : ControllerBase
{
    private readonly ILeituraService _leituraService;

    public LeituraController(ILeituraService leituraService)
    {
        _leituraService = leituraService;
    }

    [HttpPut("progresso")]
    public IActionResult AtualizarProgresso([FromQuery] int usuarioId, [FromQuery] int livroId, [FromQuery] int novaPagina, [FromBody] string comentario)
    {
        try
        {
            _leituraService.AtualizarProgresso(usuarioId, livroId, novaPagina, comentario);
            return Ok("Progresso updated.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}