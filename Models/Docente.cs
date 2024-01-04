using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Docente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Cargo { get; set; }

    public string? Correo { get; set; }

    public string? Celular { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();
}
