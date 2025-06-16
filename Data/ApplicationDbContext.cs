using Microsoft.EntityFrameworkCore;
using MarmoreGranito.API.Models;

namespace MarmoreGranito.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Bloco> Blocos { get; set; }
        public DbSet<Chapa> Chapas { get; set; }
        public DbSet<ProcessoSerragem> ProcessosSerragem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nome).HasColumnName("nome");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Senha).HasColumnName("senha");
                entity.Property(e => e.CPF).HasColumnName("CPF");
                entity.Property(e => e.Cargo).HasColumnName("cargo");
                entity.Property(e => e.Telefone).HasColumnName("Telefone");
                entity.Property(e => e.DataCadastro).HasColumnName("data_cadastro");
                entity.Property(e => e.DataUltimaAtualizacao).HasColumnName("DataUltimaAtualizacao");
                entity.Property(e => e.Ativo).HasColumnName("ativo");
            });

            modelBuilder.Entity<Bloco>(entity =>
            {
                entity.ToTable("blocos");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Codigo).HasColumnName("codigo");
                entity.Property(e => e.PedreiraOrigem).HasColumnName("pedreira_origem");
                entity.Property(e => e.TipoMaterial).HasColumnName("tipo_material");
                entity.Property(e => e.ValorCompra).HasColumnName("valor_compra");
                entity.Property(e => e.NotaFiscalEntrada).HasColumnName("nota_fiscal_entrada");
                entity.Property(e => e.DataCadastro).HasColumnName("data_cadastro");
                entity.Property(e => e.Disponivel).HasColumnName("disponivel");
                entity.Property(e => e.Cerrado).HasColumnName("Cerrado");
                entity.Property(e => e.Largura).HasColumnName("largura");
                entity.Property(e => e.Altura).HasColumnName("altura");
                entity.Property(e => e.Comprimento).HasColumnName("comprimento");
            });

            modelBuilder.Entity<Chapa>(entity =>
            {
                entity.ToTable("chapas");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BlocoId).HasColumnName("bloco_id");
                entity.Property(e => e.TipoMaterial).HasColumnName("tipo_material");
                entity.Property(e => e.Comprimento).HasColumnName("comprimento");
                entity.Property(e => e.Largura).HasColumnName("largura");
                entity.Property(e => e.Espessura).HasColumnName("espessura");
                entity.Property(e => e.ValorUnitario).HasColumnName("valor_unitario");
                entity.Property(e => e.DataCadastro).HasColumnName("data_cadastro");
                entity.Property(e => e.Disponivel).HasColumnName("disponivel");
                entity.Property(e => e.QuantidadeEstoque).HasColumnName("quantidade_estoque");

                entity.HasOne(d => d.Bloco)
                    .WithMany(p => p.Chapas)
                    .HasForeignKey(d => d.BlocoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProcessoSerragem>(entity =>
            {
                entity.ToTable("processos_serragem");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.BlocoId).HasColumnName("bloco_id");
                entity.Property(e => e.DataInicio).HasColumnName("data_inicio");
                entity.Property(e => e.QuantidadeChapas).HasColumnName("quantidade_chapas");
                entity.Property(e => e.Observacoes).HasColumnName("observacoes");

                entity.HasOne(d => d.Bloco)
                    .WithMany(p => p.ProcessosSerragem)
                    .HasForeignKey(d => d.BlocoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
} 