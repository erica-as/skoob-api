using Skoob.API.Models;

namespace Skoob.API.Services.Interfaces;

public interface ILeituraService
{
    void AtualizarProgresso(int usuarioId, int livroId, int novaPagina, string comentario);
}