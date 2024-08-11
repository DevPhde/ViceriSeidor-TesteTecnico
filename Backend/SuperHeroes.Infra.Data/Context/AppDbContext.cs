using Microsoft.EntityFrameworkCore;
using SuperHeroes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Heroi> Herois { get; set; }
        public DbSet<Superpoder> Superpoderes { get; set; }
        public DbSet<HeroiSuperpoder> HeroisSuperpoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Heroi>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.NomeHeroi)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("datetime2(7)");

                entity.Property(e => e.Altura)
                    .HasColumnType("float");

                entity.Property(e => e.Peso)
                    .HasColumnType("float");
            });

            modelBuilder.Entity<Superpoder>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.SuperpoderNome)
                    .HasColumnName("Superpoder")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<HeroiSuperpoder>(entity =>
            {
                entity.HasKey(e => new { e.HeroiId, e.SuperpoderId });

                entity.HasOne(e => e.Heroi)
                    .WithMany(h => h.HeroisSuperpoderes)
                    .HasForeignKey(e => e.HeroiId);

                entity.HasOne(e => e.Superpoderes)
                    .WithMany(s => s.HeroisSuperpoderes)
                    .HasForeignKey(e => e.SuperpoderId);
            });


            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
