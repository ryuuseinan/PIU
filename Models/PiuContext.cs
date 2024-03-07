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

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<DocenteCarrera> DocenteCarreras { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__agenda_s__3213E83FA9B16637");

            entity.ToTable("agenda_sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.AgendaSesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__agenda_se__estud__1CBC4616");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__campus__3213E83F139DC790");

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
            entity.HasKey(e => e.Id).HasName("PK__carrera__3213E83F2C9B9498");

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
                .HasConstraintName("FK__carrera__escuela__05D8E0BE");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docente__3213E83F2E8D11D6");

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

        modelBuilder.Entity<DocenteCarrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docente___3213E83F90C4E25A");

            entity.ToTable("docente_carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.DocenteId).HasColumnName("docente_id");

            entity.HasOne(d => d.Carrera).WithMany(p => p.DocenteCarreras)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__docente_c__carre__0E6E26BF");

            entity.HasOne(d => d.Docente).WithMany(p => p.DocenteCarreras)
                .HasForeignKey(d => d.DocenteId)
                .HasConstraintName("FK__docente_c__docen__0D7A0286");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83FD23A49DD");

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
                .HasConstraintName("FK__documento__estud__17036CC0");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__escuela__3213E83F1B0DF3A1");

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
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83F5C4878BB");

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
            entity.Property(e => e.CampusId).HasColumnName("campus_id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.CorreoInstitucional)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo_institucional");
            entity.Property(e => e.CorreoPersonal)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo_personal");
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

            entity.HasOne(d => d.Campus).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK__estudiant__campu__123EB7A3");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__estudiant__carre__114A936A");

            entity.HasOne(d => d.Genero).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK__estudiant__gener__14270015");

            entity.HasOne(d => d.Jornada).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.JornadaId)
                .HasConstraintName("FK__estudiant__jorna__1332DBDC");
        });

        modelBuilder.Entity<Funcionalidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__funciona__3213E83F6C652D86");

            entity.ToTable("funcionalidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genero__3213E83FAE3A5846");

            entity.ToTable("genero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Jornadum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jornada__3213E83F95FC0C5F");

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
            entity.HasKey(e => e.Id).HasName("PK__persona__3213E83FC5B63AFB");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Celular, "UQ__persona__2E4973E7FBE53B80").IsUnique();

            entity.HasIndex(e => e.Rut, "UQ__persona__C2B74E76E84FD012").IsUnique();

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
                .HasConstraintName("FK__persona__usuario__0A9D95DB");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83F2C3586DC");

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
                        .HasConstraintName("FK__funcional__funci__7E37BEF6"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__rol_i__7D439ABD"),
                    j =>
                    {
                        j.HasKey("RolId", "FuncionalidadId").HasName("PK__funciona__9C554FB04B219215");
                        j.ToTable("funcionalidad_rol");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_id");
                        j.IndexerProperty<int>("FuncionalidadId").HasColumnName("funcionalidad_id");
                    });
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sesion__3213E83F5A0952E6");

            entity.ToTable("sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccionCierre)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("accion_cierre");
            entity.Property(e => e.AccionDesarrollo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("accion_desarrollo");
            entity.Property(e => e.AccionInicio)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("accion_inicio");
            entity.Property(e => e.Asistio).HasColumnName("asistio");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.HoraTermino)
                .HasColumnType("datetime")
                .HasColumnName("hora_termino");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("objetivo");
            entity.Property(e => e.ObservacionCierre)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("observacion_cierre");
            entity.Property(e => e.ObservacionDesarrollo)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("observacion_desarrollo");
            entity.Property(e => e.ObservacionInicio)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("observacion_inicio");
            entity.Property(e => e.ViaContacto)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("via_contacto");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Sesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__sesion__estudian__19DFD96B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83F5AD9AC23");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "UQ__usuario__2A586E0BFBBDE15E").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__usuario__72AFBCC67E094B9F").IsUnique();

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
                .HasConstraintName("FK__usuario__rol_id__02FC7413");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
