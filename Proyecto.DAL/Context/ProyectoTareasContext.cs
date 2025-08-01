using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Proyecto.ML.Entities;

namespace Proyecto.DAL.Context;

public partial class ProyectoTareasContext : DbContext
{
    public ProyectoTareasContext()
    {
    }

    public ProyectoTareasContext(DbContextOptions<ProyectoTareasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<EstadoTarea> EstadoTareas { get; set; }

    public virtual DbSet<Prioridad> Prioridads { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=ProyectoTareas;User Id=sa;Password=password;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__86989FB231BA5041");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Estado1)
                .HasMaxLength(20)
                .HasColumnName("estado");
        });

        modelBuilder.Entity<EstadoTarea>(entity =>
        {
            entity.HasKey(e => e.IdEstadoTarea).HasName("PK__EstadoTa__349F50C59650747C");

            entity.ToTable("EstadoTarea");

            entity.Property(e => e.IdEstadoTarea).HasColumnName("id_estado_tarea");
            entity.Property(e => e.EstadoTarea1)
                .HasMaxLength(50)
                .HasColumnName("estado_tarea");
        });

        modelBuilder.Entity<Prioridad>(entity =>
        {
            entity.HasKey(e => e.IdPrioridad).HasName("PK__Priorida__EF3DAB409ED0E430");

            entity.ToTable("Prioridad");

            entity.Property(e => e.IdPrioridad).HasColumnName("id_prioridad");
            entity.Property(e => e.Prioridad1)
                .HasMaxLength(20)
                .HasColumnName("prioridad");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Rolid).HasName("PK__Roles__5403326CB4D051D2");

            entity.Property(e => e.Rolid).HasColumnName("rolid");
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .HasColumnName("rol");
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.IdTarea).HasName("PK__Tarea__C0ECF707C3932C61");

            entity.ToTable("Tarea");

            entity.Property(e => e.IdTarea).HasColumnName("id_tarea");
            entity.Property(e => e.CreadaPor).HasColumnName("creada_por");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.FechaHoraSolicitud)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_solicitud");
            entity.Property(e => e.FechaHoraUpdate)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_update");
            entity.Property(e => e.IdEstadoTarea).HasColumnName("id_estado_tarea");
            entity.Property(e => e.IdPrioridad).HasColumnName("id_prioridad");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.UpdatePor).HasColumnName("update_por");
            //////////////////
            entity.HasOne(d => d.CreadaPorNavigation).WithMany(p => p.TareaCreadaPorNavigations)
                .HasForeignKey(d => d.CreadaPor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarea_CreadaPor");

            entity.HasOne(d => d.IdEstadoTareaNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdEstadoTarea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarea_EstadoTarea");

            entity.HasOne(d => d.IdPrioridadNavigation).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdPrioridad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarea_Prioridad");
            /////////////////
            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.TareaIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tarea_Usuario");

            entity.HasOne(d => d.UpdatePorNavigation).WithMany(p => p.TareaUpdatePorNavigations)
                .HasForeignKey(d => d.UpdatePor)
                .HasConstraintName("FK_Tarea_UpdatePor");
        });
        ////////////////
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04AD66CE6AD1");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__AB6E6164427F5C7D").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Rolid).HasColumnName("rolid");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Estado");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rolid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
