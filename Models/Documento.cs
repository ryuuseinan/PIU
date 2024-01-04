using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Documento
{
    public int Id { get; set; }

    public int? EstudianteId { get; set; }

    public string? TipoArchivo { get; set; }

    public string? ArchivoAdjunto { get; set; }

    public virtual Estudiante? Estudiante { get; set; }
}
