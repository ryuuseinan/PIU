using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Carrera
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? EscuelaId { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<DocenteCarrera> DocenteCarreras { get; set; } = new List<DocenteCarrera>();

    public virtual Escuela? Escuela { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
