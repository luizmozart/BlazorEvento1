using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorEvento1.Data
{
    public partial class BlazorEFContext : DbContext
    {
        public BlazorEFContext()
        {
        }

        public BlazorEFContext(DbContextOptions<BlazorEFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<EventoParticipante> EventoParticipante { get; set; }
        public virtual DbSet<Participante> Participante { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-EIL115B;Initial Catalog=BlazorEF;User ID=sa;Password=kbce63y");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Evento>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EventoParticipante>(entity =>
            {
                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.EventoParticipante)
                    .HasForeignKey(d => d.IdEvento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventoParticipante_Evento");

                entity.HasOne(d => d.IdParticipanteNavigation)
                    .WithMany(p => p.EventoParticipante)
                    .HasForeignKey(d => d.IdParticipante)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventoParticipante_Participante");
            });

            modelBuilder.Entity<Participante>(entity =>
            {
                entity.Property(e => e.Bairro).HasMaxLength(50);

                entity.Property(e => e.Cep).HasMaxLength(8);

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasColumnName("CPF")
                    .HasMaxLength(11);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Telefone).HasMaxLength(8);

                entity.Property(e => e.Whatsapp)
                    .IsRequired()
                    .HasMaxLength(9);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
