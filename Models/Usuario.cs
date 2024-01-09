using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public string? Salt { get; set; }

    public int? RolId { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();

    public virtual Rol? Rol { get; set; }
}
