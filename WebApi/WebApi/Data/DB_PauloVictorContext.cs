using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.models;

namespace WebApi.Data
{
    public partial class DB_PauloVictorContext : DbContext
    {
        public DB_PauloVictorContext()
        {
        }

        public DB_PauloVictorContext(DbContextOptions<DB_PauloVictorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ativo> Ativo { get; set; }
        public virtual DbSet<Ordem> Ordem { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
        //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=meuservidorsql.database.windows.net;Initial Catalog=DB_PauloVictor;User ID=azureuser;Password=Password01 ;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ativo>(entity =>
            {
                entity.HasKey(e => e.IdAtivo);

               entity.Property(e => e.IdAtivo).HasColumnName("id_ativo");

                entity.Property(e => e.Descricao)
                    .HasColumnName("descricao")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LoteMinimo).HasColumnName("lote_minimo");
            });

            modelBuilder.Entity<Ordem>(entity =>
            {
                entity.HasKey(e => e.IdOrdem);

                entity.Property(e => e.IdOrdem).HasColumnName("id_ordem");

                entity.Property(e => e.ClasseNegociacao)
                    .IsRequired()
                    .HasColumnName("classe_negociacao")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Data)
                    .HasColumnName("data")
                    .HasColumnType("date");

                entity.Property(e => e.FkIdAtivo).HasColumnName("fk_id_ativo");

                entity.Property(e => e.Preco).HasColumnName("preco");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

               
            });
        }
    }
}
