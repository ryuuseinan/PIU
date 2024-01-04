using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string? Rut { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string? Correo { get; set; }

    public string? Celular { get; set; }

    public int? IngresoPiu { get; set; }

    public int? EgresoPiu { get; set; }

    public int? CarreraId { get; set; }

    public int? CampusId { get; set; }

    public int? JornadaId { get; set; }

    public int? GeneroId { get; set; }

    public int? AsignaturaId { get; set; }

    public string? Foto { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<AgendaSesion> AgendaSesions { get; set; } = new List<AgendaSesion>();

    public virtual Asignatura? Asignatura { get; set; }

    public virtual Campus? Campus { get; set; }

    public virtual Carrera? Carrera { get; set; }

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual Anio? EgresoPiuNavigation { get; set; }

    public virtual Genero? Genero { get; set; }

    public virtual Anio? IngresoPiuNavigation { get; set; }

    public virtual Jornadum? Jornada { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
