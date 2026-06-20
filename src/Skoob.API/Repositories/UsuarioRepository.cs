using Microsoft.EntityFrameworkCore;
using Skoob.API.Data;
using Skoob.API.Models;
using Skoob.API.Repositories.Interfaces;

namespace Skoob.API.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SkoobDbContext _context;

    public UsuarioRepository(SkoobDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Usuario> ObterTodos()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario? ObterPorId(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Id == id);
    }
    
    public Usuario? ObterComEstante(int usuarioId)
    {
        return _context.Usuarios
            .Include(u => u.Estante)
            .ThenInclude(e => e.Livro) 
            .Include(u => u.Estante)
            .ThenInclude(e => e.Avaliacao) 
            .FirstOrDefault(u => u.Id == usuarioId);
    }
    
    public Usuario? ObterPorEmail(string email)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
    }

    public void Adicionar(Usuario entidade)
    {
        _context.Usuarios.Add(entidade);
        _context.SaveChanges(); 
    }

    public void Atualizar(Usuario entidade)
    {
        _context.Usuarios.Update(entidade);
        _context.SaveChanges(); 
    }
}