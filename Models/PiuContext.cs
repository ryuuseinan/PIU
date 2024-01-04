using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PIU.Models;

public partial class PiuContext : DbContext
{
    public PiuContext()
    {
    }

    public PiuContext(DbContextOptions<PiuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AgendaSesion> AgendaSesions { get; set; }

    public virtual DbSet<Anio> Anios { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AsignaturaCarrera> AsignaturaCarreras { get; set; }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<DetalleSesion> DetalleSesions { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Escuela> Escuelas { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Funcionalidad> Funcionalidads { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Jornadum> Jornada { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgendaSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__agenda_s__3213E83F71D70FD2");

            entity.ToTable("agenda_sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.AgendaSesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__agenda_se__estud__3F6663D5");
        });

        modelBuilder.Entity<Anio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__anio__3213E83F35A9B988");

            entity.ToTable("anio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83FDF1F70B6");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.DocenteId).HasColumnName("docente_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__asignatur__carre__297722B6");

            entity.HasOne(d => d.Docente).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.DocenteId)
                .HasConstraintName("FK__asignatur__docen__2A6B46EF");
        });

        modelBuilder.Entity<AsignaturaCarrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83FE56B6784");

            entity.ToTable("asignatura_carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AsignaturaId).HasColumnName("asignatura_id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.AsignaturaCarreras)
                .HasForeignKey(d => d.AsignaturaId)
                .HasConstraintName("FK__asignatur__asign__2E3BD7D3");

            entity.HasOne(d => d.Carrera).WithMany(p => p.AsignaturaCarreras)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__asignatur__carre__2D47B39A");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__campus__3213E83F1E88C287");

            entity.ToTable("campus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carrera__3213E83F754AC9B7");

            entity.ToTable("carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.EscuelaId).HasColumnName("escuela_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Escuela).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.EscuelaId)
                .HasConstraintName("FK__carrera__escuela__269AB60B");
        });

        modelBuilder.Entity<DetalleSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalle___3213E83F357056BA");

            entity.ToTable("detalle_sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("acciones");
            entity.Property(e => e.Etapa)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("etapa");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("observaciones");
            entity.Property(e => e.SesionId).HasColumnName("sesion_id");

            entity.HasOne(d => d.Sesion).WithMany(p => p.DetalleSesions)
                .HasForeignKey(d => d.SesionId)
                .HasConstraintName("FK__detalle_s__sesio__4242D080");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docente__3213E83FEA2ED96A");

            entity.ToTable("docente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Cargo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Celular)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83F9A0443D4");

            entity.ToTable("documento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ArchivoAdjunto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("archivo_adjunto");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.TipoArchivo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("tipo_archivo");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__documento__estud__39AD8A7F");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__escuela__3213E83FA0372AFC");

            entity.ToTable("escuela");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83FCC00DB59");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.AsignaturaId).HasColumnName("asignatura_id");
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EgresoPiu).HasColumnName("egreso_piu");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("datetime")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.GeneroId).HasColumnName("genero_id");
            entity.Property(e => e.IngresoPiu).HasColumnName("ingreso_piu");
            entity.Property(e => e.JornadaId).HasColumnName("jornada_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rut)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rut");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.AsignaturaId)
                .HasConstraintName("FK__estudiant__asign__36D11DD4");

            entity.HasOne(d => d.Campus).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK__estudiant__campu__33F4B129");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__estudiant__carre__33008CF0");

            entity.HasOne(d => d.EgresoPiuNavigation).WithMany(p => p.EstudianteEgresoPiuNavigations)
                .HasForeignKey(d => d.EgresoPiu)
                .HasConstraintName("FK__estudiant__egres__320C68B7");

            entity.HasOne(d => d.Genero).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK__estudiant__gener__35DCF99B");

            entity.HasOne(d => d.IngresoPiuNavigation).WithMany(p => p.EstudianteIngresoPiuNavigations)
                .HasForeignKey(d => d.IngresoPiu)
                .HasConstraintName("FK__estudiant__ingre__3118447E");

            entity.HasOne(d => d.Jornada).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.JornadaId)
                .HasConstraintName("FK__estudiant__jorna__34E8D562");
        });

        modelBuilder.Entity<Funcionalidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__funciona__3213E83FDED62B09");

            entity.ToTable("funcionalidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genero__3213E83FAB107D77");

            entity.ToTable("genero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Jornadum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jornada__3213E83F1BBC4C86");

            entity.ToTable("jornada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83FC8D6B6C3");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasMany(d => d.Funcionalidads).WithMany(p => p.Rols)
                .UsingEntity<Dictionary<string, object>>(
                    "FuncionalidadRol",
                    r => r.HasOne<Funcionalidad>().WithMany()
                        .HasForeignKey("FuncionalidadId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__funci__23BE4960"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__rol_i__22CA2527"),
                    j =>
                    {
                        j.HasKey("RolId", "FuncionalidadId").HasName("PK__funciona__9C554FB0C29FAB38");
                        j.ToTable("funcionalidad_rol");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_id");
                        j.IndexerProperty<int>("FuncionalidadId").HasColumnName("funcionalidad_id");
                    });
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sesion__3213E83FB67364E6");

            entity.ToTable("sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Asistio).HasColumnName("asistio");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("objetivo");
            entity.Property(e => e.ViaContacto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("via_contacto");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__sesion__estudian__3C89F72A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
