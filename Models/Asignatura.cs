using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public int? CarreraId { get; set; }

    public int? DocenteId { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<AsignaturaCarrera> AsignaturaCarreras { get; set; } = new List<AsignaturaCarrera>();

    public virtual Carrera? Carrera { get; set; }

    public virtual Docente? Docente { get; set; }

    public virtual ICollection<Estudiante> Estudiantes { get; set; } = new List<Estudiante>();
}
