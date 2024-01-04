using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Escuela
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Carrera> Carreras { get; set; } = new List<Carrera>();
}
