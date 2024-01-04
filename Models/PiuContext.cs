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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("SERVER=localhost,1433;DATABASE=piu;UID=sa;PWD=SQL#1234;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AgendaSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__agenda_s__3213E83FD2F1E77D");

            entity.ToTable("agenda_sesion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstudianteId).HasColumnName("estudiante_id");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.AgendaSesions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("FK__agenda_se__estud__75A278F5");
        });

        modelBuilder.Entity<Anio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__anio__3213E83F08DEDF53");

            entity.ToTable("anio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83FE0B76E9E");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.DocenteId).HasColumnName("docente_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__asignatur__carre__5FB337D6");

            entity.HasOne(d => d.Docente).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.DocenteId)
                .HasConstraintName("FK__asignatur__docen__60A75C0F");
        });

        modelBuilder.Entity<AsignaturaCarrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83F1EEB0F89");

            entity.ToTable("asignatura_carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AsignaturaId).HasColumnName("asignatura_id");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.AsignaturaCarreras)
                .HasForeignKey(d => d.AsignaturaId)
                .HasConstraintName("FK__asignatur__asign__6477ECF3");

            entity.HasOne(d => d.Carrera).WithMany(p => p.AsignaturaCarreras)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__asignatur__carre__6383C8BA");
        });

        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__campus__3213E83F1FDF8C75");

            entity.ToTable("campus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreCampus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_campus");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carrera__3213E83FA71C13D1");

            entity.ToTable("carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EscuelaId).HasColumnName("escuela_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.Escuela).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.EscuelaId)
                .HasConstraintName("FK__carrera__escuela__5CD6CB2B");
        });

        modelBuilder.Entity<DetalleSesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__detalle___3213E83F5665867C");

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
                .HasConstraintName("FK__detalle_s__sesio__787EE5A0");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__docente__3213E83F881B7681");

            entity.ToTable("docente");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83FBA3EC16A");

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
                .HasConstraintName("FK__documento__estud__6FE99F9F");
        });

        modelBuilder.Entity<Escuela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__escuela__3213E83F8AE4C260");

            entity.ToTable("escuela");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83FA3916558");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id).HasColumnName("id");
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
                .HasConstraintName("FK__estudiant__asign__6D0D32F4");

            entity.HasOne(d => d.Campus).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CampusId)
                .HasConstraintName("FK__estudiant__campu__6A30C649");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("FK__estudiant__carre__693CA210");

            entity.HasOne(d => d.EgresoPiuNavigation).WithMany(p => p.EstudianteEgresoPiuNavigations)
                .HasForeignKey(d => d.EgresoPiu)
                .HasConstraintName("FK__estudiant__egres__68487DD7");

            entity.HasOne(d => d.Genero).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.GeneroId)
                .HasConstraintName("FK__estudiant__gener__6C190EBB");

            entity.HasOne(d => d.IngresoPiuNavigation).WithMany(p => p.EstudianteIngresoPiuNavigations)
                .HasForeignKey(d => d.IngresoPiu)
                .HasConstraintName("FK__estudiant__ingre__6754599E");

            entity.HasOne(d => d.Jornada).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.JornadaId)
                .HasConstraintName("FK__estudiant__jorna__6B24EA82");
        });

        modelBuilder.Entity<Funcionalidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__funciona__3213E83F1928D6D1");

            entity.ToTable("funcionalidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__genero__3213E83F511880CD");

            entity.ToTable("genero");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Jornadum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jornada__3213E83F1C904A48");

            entity.ToTable("jornada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rol__3213E83F9ACC68F4");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");

            entity.HasMany(d => d.Funcionalidads).WithMany(p => p.Rols)
                .UsingEntity<Dictionary<string, object>>(
                    "FuncionalidadRol",
                    r => r.HasOne<Funcionalidad>().WithMany()
                        .HasForeignKey("FuncionalidadId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__funci__59FA5E80"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__funcional__rol_i__59063A47"),
                    j =>
                    {
                        j.HasKey("RolId", "FuncionalidadId").HasName("PK__funciona__9C554FB08E9E6E72");
                        j.ToTable("funcionalidad_rol");
                        j.IndexerProperty<int>("RolId").HasColumnName("rol_id");
                        j.IndexerProperty<int>("FuncionalidadId").HasColumnName("funcionalidad_id");
                    });
        });

        modelBuilder.Entity<Sesion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sesion__3213E83F9BF8997A");

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
                .HasConstraintName("FK__sesion__estudian__72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
