using Microsoft.EntityFrameworkCore;
using Test_API.Models.Entities;

namespace Test_API.Migrations;

public partial class TestRdpContext : DbContext
{
    public TestRdpContext()
    {
    }

    public TestRdpContext(DbContextOptions<TestRdpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__3CD5687E3A494924");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura").ValueGeneratedOnAdd();
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.PersonaId).HasColumnName("personaId");

            entity.HasOne(d => d.Persona).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.PersonaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Factura__persona__398D8EEE");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__A4788141C5E3AFDC");

            entity.ToTable("Persona");

            entity.Property(e => e.IdPersona).HasColumnName("idPersona").ValueGeneratedOnAdd();
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoMaterno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidoPaterno");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
