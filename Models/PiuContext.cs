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

    public virtual DbSet<DetalleSesion> DetalleSesions { get; set; }

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
            entity.HasKey(e => e.Id).HasName("PK__agenda_s__3213E83FB5B614B3");

            entity.ToTable("agenda_sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.AgendaSesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__agenda_se__estud__6477ECF3");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__campus__3213E83F32076BA0");

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
            entity.HasKey(e => e.Id).HasName("PK__carrera__3213E83FC9F047CE");

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
                .HasConstraintName("FK__carrera__escuela__4D94879B");
        });

        modelBuilder.Entity<DetalleSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalle___3213E83F2DF22B39");

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
                .HasConstraintName("FK__detalle_s__sesio__6754599E");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docente__3213E83FD40CF3E8");

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
            entity.HasKey(e => e.Id).HasName("PK__docente___3213E83F54157F30");

            entity.ToTable("docente_carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.DocenteId).HasColumnName("docente_id");

            entity.HasOne(d => d.Carrera).WithMany(p => p.DocenteCarreras)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__docente_c__carre__5629CD9C");

            entity.HasOne(d => d.Docente).WithMany(p => p.DocenteCarreras)
                .HasForeignKey(d => d.DocenteId)
                .HasConstraintName("FK__docente_c__docen__5535A963");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83F05F110DF");

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
                .HasConstraintName("FK__documento__estud__5EBF139D");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__escuela__3213E83F7CBB0A57");

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
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83F87241378");

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

            entity.HasOne(d => d.Campus).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK__estudiant__campu__59FA5E80");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__estudiant__carre__59063A47");

            entity.HasOne(d => d.Genero).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK__estudiant__gener__5BE2A6F2");

            entity.HasOne(d => d.Jornada).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.JornadaId)
                .HasConstraintName("FK__estudiant__jorna__5AEE82B9");
        });

        modelBuilder.Entity<Funcionalidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__funciona__3213E83F94E3DAB8");

            entity.ToTable("funcionalidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genero__3213E83F142E0E73");

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
            entity.HasKey(e => e.Id).HasName("PK__jornada__3213E83FF0AE260F");

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
            entity.HasKey(e => e.Id).HasName("PK__persona__3213E83FBE8D9876");

            entity.ToTable("persona");

            entity.HasIndex(e => e.Celular, "UQ__persona__2E4973E73CF0DC9B").IsUnique();

            entity.HasIndex(e => e.Rut, "UQ__persona__C2B74E76CDE46815").IsUnique();

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
                .HasConstraintName("FK__persona__usuario__52593CB8");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83FB70191C1");

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
                        .HasConstraintName("FK__funcional__funci__45F365D3"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__rol_i__44FF419A"),
                    j =>
                    {
                        j.HasKey("RolId", "FuncionalidadId").HasName("PK__funciona__9C554FB04ED857D6");
                        j.ToTable("funcionalidad_rol");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_id");
                        j.IndexerProperty<int>("FuncionalidadId").HasColumnName("funcionalidad_id");
                    });
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sesion__3213E83FB89E6438");

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
                .HasConstraintName("FK__sesion__estudian__619B8048");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuario__3213E83FD6B7A276");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "UQ__usuario__2A586E0B175011EC").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__usuario__72AFBCC664CEFEB8").IsUnique();

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
                .HasConstraintName("FK__usuario__rol_id__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
