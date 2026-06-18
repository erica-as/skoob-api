using Microsoft.EntityFrameworkCore;
using Skoob.API.Models;

namespace Skoob.API.Data;

public class SkoobDbContext : DbContext
{
    public SkoobDbContext(DbContextOptions<SkoobDbContext> options) : base(options)
    {
    }

    // Tabelas que serão criadas no Banco de Dados
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<EstanteLivro> EstantesLivros { get; set; }
    public DbSet<HistoricoLeitura> HistoricosLeituras { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Aqui você pode adicionar configurações de chaves e relacionamentos se necessário
    }
}