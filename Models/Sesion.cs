using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Sesion
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaTermino { get; set; }

    public string? ViaContacto { get; set; }

    public string? Objetivo { get; set; }

    public string? ObservacionInicio { get; set; }

    public string? ObservacionDesarrollo { get; set; }

    public string? ObservacionCierre { get; set; }

    public string? AccionInicio { get; set; }

    public string? AccionDesarrollo { get; set; }

    public string? AccionCierre { get; set; }

    public bool? Asistio { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
}
