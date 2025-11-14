using Microsoft.EntityFrameworkCore;
using AnalizadorSoftware.Models;

namespace AnalizadorSoftware.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Proceso> Procesos { get; set; }
    public DbSet<Subproceso> Subprocesos { get; set; }
    public DbSet<CasoUso> CasosUso { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proceso>(entity =>
        {
            entity.ToTable("proceso");
            entity.HasKey(e => e.IdProceso);
            entity.Property(e => e.IdProceso).HasColumnName("id_proceso");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
        });

        modelBuilder.Entity<Subproceso>(entity =>
        {
            entity.ToTable("subproceso");
            entity.HasKey(e => e.IdSubproceso);
            entity.Property(e => e.IdSubproceso).HasColumnName("id_subproceso");
            entity.Property(e => e.IdProceso).HasColumnName("id_proceso");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            
            entity.HasOne(e => e.Proceso)
                .WithMany(p => p.Subprocesos)
                .HasForeignKey(e => e.IdProceso);
        });

        modelBuilder.Entity<CasoUso>(entity =>
        {
            entity.ToTable("caso_uso");
            entity.HasKey(e => e.IdCasoUso);
            entity.Property(e => e.IdCasoUso).HasColumnName("id_caso_uso");
            entity.Property(e => e.IdSubproceso).HasColumnName("id_subproceso");
            entity.Property(e => e.Nombre).HasColumnName("nombre").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.ActorPrincipal).HasColumnName("actor_principal").HasMaxLength(150);
            entity.Property(e => e.TipoCasoUso).HasColumnName("tipo_caso_uso");
            entity.Property(e => e.Precondiciones).HasColumnName("precondiciones");
            entity.Property(e => e.Postcondiciones).HasColumnName("postcondiciones");
            entity.Property(e => e.CriteriosDeAceptacion).HasColumnName("criterios_de_aceptacion");
            
            entity.HasOne(e => e.Subproceso)
                .WithMany(s => s.CasosUso)
                .HasForeignKey(e => e.IdSubproceso);
        });
    }
}
