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

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sesion> Sesions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgendaSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__agenda_s__3213E83F933B8704");

            entity.ToTable("agenda_sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.AgendaSesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__agenda_se__estud__149C0161");
        });

        modelBuilder.Entity<Anio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__anio__3213E83FE652BE9C");

            entity.ToTable("anio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83F8E8CB8A6");

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
                .HasConstraintName("FK__asignatur__carre__7EACC042");

            entity.HasOne(d => d.Docente).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.DocenteId)
                .HasConstraintName("FK__asignatur__docen__7FA0E47B");
        });

        modelBuilder.Entity<AsignaturaCarrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83FA74E34F8");

            entity.ToTable("asignatura_carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AsignaturaId).HasColumnName("asignatura_id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.AsignaturaCarreras)
                .HasForeignKey(d => d.AsignaturaId)
                .HasConstraintName("FK__asignatur__asign__0371755F");

            entity.HasOne(d => d.Carrera).WithMany(p => p.AsignaturaCarreras)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__asignatur__carre__027D5126");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__campus__3213E83FDF75B429");

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
            entity.HasKey(e => e.Id).HasName("PK__carrera__3213E83F3C6AB779");

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
                .HasConstraintName("FK__carrera__escuela__770B9E7A");
        });

        modelBuilder.Entity<DetalleSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalle___3213E83F83DA55ED");

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
                .HasConstraintName("FK__detalle_s__sesio__17786E0C");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docente__3213E83F686CC394");

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
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83F61057689");

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
                .HasConstraintName("FK__documento__estud__0EE3280B");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__escuela__3213E83FF6AAEAFE");

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
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83F35AA70B9");

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
                .HasConstraintName("FK__estudiant__asign__0C06BB60");

            entity.HasOne(d => d.Campus).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK__estudiant__campu__092A4EB5");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__estudiant__carre__08362A7C");

            entity.HasOne(d => d.EgresoPiuNavigation).WithMany(p => p.EstudianteEgresoPiuNavigations)
                .HasForeignKey(d => d.EgresoPiu)
                .HasConstraintName("FK__estudiant__egres__07420643");

            entity.HasOne(d => d.Genero).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK__estudiant__gener__0B129727");

            entity.HasOne(d => d.IngresoPiuNavigation).WithMany(p => p.EstudianteIngresoPiuNavigations)
                .HasForeignKey(d => d.IngresoPiu)
                .HasConstraintName("FK__estudiant__ingre__064DE20A");

            entity.HasOne(d => d.Jornada).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.JornadaId)
                .HasConstraintName("FK__estudiant__jorna__0A1E72EE");
        });

        modelBuilder.Entity<Funcionalidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__funciona__3213E83F742229D1");

            entity.ToTable("funcionalidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genero__3213E83F12D42C55");

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
            entity.HasKey(e => e.Id).HasName("PK__jornada__3213E83F834517C8");

            entity.ToTable("jornada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__persona__3213E83F7A5FF41B");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Celular, "UQ__persona__2E4973E782DC6DFE").IsUnique();

            entity.HasIndex(e => e.Rut, "UQ__persona__C2B74E763B6406E3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido_materno");
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido_paterno");
            entity.Property(e => e.Celular)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rut)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("rut");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Personas)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__persona__usuario__7BD05397");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83F127A9709");

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
                        .HasConstraintName("FK__funcional__funci__6F6A7CB2"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__rol_i__6E765879"),
                    j =>
                    {
                        j.HasKey("RolId", "FuncionalidadId").HasName("PK__funciona__9C554FB0000A3F2B");
                        j.ToTable("funcionalidad_rol");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_id");
                        j.IndexerProperty<int>("FuncionalidadId").HasColumnName("funcionalidad_id");
                    });
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sesion__3213E83F5E185B0C");

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
                .HasConstraintName("FK__sesion__estudian__11BF94B6");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F570F355F");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "UQ__usuario__2A586E0BDA934429").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__usuario__72AFBCC64233C303").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("salt");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__usuario__rol_id__742F31CF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
