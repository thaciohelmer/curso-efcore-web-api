using EFCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.WebAPI.Data
{
    public class HeroiContexto : DbContext
    {
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Batalha> Batalhas { get; set; }
        public DbSet<Arma> Armas { get; set; }

        public DbSet<HeroiBatalha> HeroisBatalhas { get; set; }
        public DbSet<IdentidadeSecreta> IdentidadeSecretas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=WKS-DEV178-DGM;user id=sa;password=cidadao;database=HeroApp");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiBatalha>(entity =>
                {
                    entity.HasKey(e => new { e.BatalhaId, e.HeroiId });
                });
        }
    }
}
