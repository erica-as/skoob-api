using Microsoft.EntityFrameworkCore;
using Skoob.API.Models;
using Skoob.API.Services.Interfaces;
using Skoob.API.Data; 

namespace Skoob.API.Services;

public class LivroService : ILivroService
{
    private readonly SkoobDbContext _context;

    public LivroService(SkoobDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Livro>> BuscarLivros()
    {
        return await _context.Livros.ToListAsync();
    }

    public async Task<Livro> BuscarLivrosIdc(int id)
    {
        return await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<bool> CriarLivros(Livro livro)
    {
        await _context.Livros.AddAsync(livro);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> AtualizarLivros(int id, Livro livro)
    {
        var livroExistente = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
        
        if (livroExistente == null)
            return false;
        
        livroExistente.Titulo = livro.Titulo;
        livroExistente.Autor = livro.Autor;
        livroExistente.TotalPaginas = livro.TotalPaginas;

        _context.Livros.Update(livroExistente);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> DeletarLivros(int id)
    {
        var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
        
        if (livro == null)
            return false;

        _context.Livros.Remove(livro);
        var rowsAffected = await _context.SaveChangesAsync();
        return rowsAffected > 0;
    }
}