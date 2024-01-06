using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Persona
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public string Rut { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
