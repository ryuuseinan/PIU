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

    public short? IngresoPiu { get; set; }

    public short? EgresoPiu { get; set; }

    public int? CarreraId { get; set; }

    public int? CampusId { get; set; }

    public int? JornadaId { get; set; }

    public int? GeneroId { get; set; }

    public string? Foto { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<AgendaSesion> AgendaSesions { get; set; } = new List<AgendaSesion>();

    public virtual Campus? Campus { get; set; }

    public virtual Carrera? Carrera { get; set; }

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual Genero? Genero { get; set; }

    public virtual Jornadum? Jornada { get; set; }

    public virtual ICollection<Sesion> Sesions { get; set; } = new List<Sesion>();
}
