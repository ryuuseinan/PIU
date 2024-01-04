using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Funcionalidad
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
