using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Carrera
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? EscuelaId { get; set; }

    public virtual ICollection<AsignaturaCarrera> AsignaturaCarreras { get; set; } = new List<AsignaturaCarrera>();

    public virtual ICollection<Asignatura> Asignaturas { get; set; } = new List<Asignatura>();

    public virtual Escuela? Escuela { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
