using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Anio
{
    public int Id { get; set; }

    public int? Nombre { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Estudiante> EstudianteEgresoPiuNavigations { get; set; } = new List<Estudiante>();

    public virtual ICollection<Estudiante> EstudianteIngresoPiuNavigations { get; set; } = new List<Estudiante>();
}
