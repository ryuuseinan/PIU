using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class AsignaturaCarrera
{
    public int Id { get; set; }

    public int? CarreraId { get; set; }

    public int? AsignaturaId { get; set; }

    public virtual Asignatura? Asignatura { get; set; }

    public virtual Carrera? Carrera { get; set; }
}
