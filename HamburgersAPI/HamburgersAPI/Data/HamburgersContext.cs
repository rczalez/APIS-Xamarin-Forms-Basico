using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HamburgersAPI.Entities.Models
{
    public partial class HamburgersContext : DbContext
    {
        public HamburgersContext()
        {
        }

        public HamburgersContext(DbContextOptions<HamburgersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contacto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:hamburgerserver.database.windows.net,1433;Initial Catalog=HamburgersDB;Persist Security Info=False;User ID=rotiux;Password=titos95r.g;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Contacto)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contacto_Usuario");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ColorFavorito)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FechaIngreso).HasColumnType("datetime");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Sexo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
