using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class DetalleSesion
{
    public int Id { get; set; }

    public int? SesionId { get; set; }

    public string? Etapa { get; set; }

    public string? Acciones { get; set; }

    public string? Observaciones { get; set; }

    public virtual Sesion? Sesion { get; set; }
}
