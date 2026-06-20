using Microsoft.EntityFrameworkCore;
using Skoob.API.Models;

namespace Skoob.API.Data;

public class SkoobDbContext : DbContext
{
    public SkoobDbContext(DbContextOptions<SkoobDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<EstanteLivro> EstantesLivros { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; } // Removido o DbSet de HistoricoLeitura

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}