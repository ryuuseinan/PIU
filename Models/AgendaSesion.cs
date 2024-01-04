using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class AgendaSesion
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
}
