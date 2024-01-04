using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Jornadum
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
