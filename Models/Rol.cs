using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string? NombreRol { get; set; }

    public virtual ICollection<Funcionalidad> Funcionalidads { get; set; } = new List<Funcionalidad>();
}
