using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CursoLineaAPI.Modelos;

public partial class CursoLineaContext : DbContext
{
    public CursoLineaContext()
    {
    }

    public CursoLineaContext(DbContextOptions<CursoLineaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-11O6E15\\MYSQLSERVER; Database=curso_linea; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Curso__7E023A37ECA544CD");

            entity.ToTable("Curso");

            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");

            entity.HasOne(d => d.Profesor).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.ProfesorId)
                .HasConstraintName("FK__Curso__ProfesorI__3B75D760");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F768338D3A3F1CA");

            entity.ToTable("Estudiante");

            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Curso).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CursoId)
                .HasConstraintName("FK__Estudiant__Curso__3C69FB99");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.ProfesorId).HasName("PK__Profesor__4DF3F028A1E05D43");

            entity.ToTable("Profesor");

            entity.Property(e => e.ProfesorId).HasColumnName("ProfesorID");
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
