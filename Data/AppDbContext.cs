using Microsoft.EntityFrameworkCore;
using PimVIII.Api.Entities; // Importa nossas entidades

namespace PimVIII.Api.Data
{
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração (injeção de dependência)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Mapeia nossas entidades para tabelas do banco
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Criador> Criadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da Relação N-para-N (Muitos-para-Muitos)
            // Entre Playlist e Conteudo
            // O EF Core irá criar automaticamente a tabela "ItemPlaylist"
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Conteudos)
                .WithMany(c => c.Playlists)
                .UsingEntity(j => j.ToTable("ItemPlaylist")); // Nomeia a tabela associativa

            // Configuração da Relação 1-para-N (Usuario -> Playlist)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioID); // Chave estrangeira

            // Configuração da Relação 1-para-N (Criador -> Conteudo)
            modelBuilder.Entity<Criador>()
                .HasMany(c => c.Conteudos)
                .WithOne(co => co.Criador)
                .HasForeignKey(co => co.CriadorID); // Chave estrangeira
        }
    }
}