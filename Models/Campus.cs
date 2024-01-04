using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Campus
{
    public int Id { get; set; }

    public string? NombreCampus { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
