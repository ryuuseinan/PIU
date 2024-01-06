using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Funcionalidad> Funcionalidads { get; set; } = new List<Funcionalidad>();
}
