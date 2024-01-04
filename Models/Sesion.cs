using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Sesion
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public DateTime? Fecha { get; set; }

    public string? ViaContacto { get; set; }

    public string? Objetivo { get; set; }

    public bool? Asistio { get; set; }

    public virtual ICollection<DetalleSesion> DetalleSesions { get; set; } = new List<DetalleSesion>();

    public virtual Estudiante? Estudiante { get; set; }
}
