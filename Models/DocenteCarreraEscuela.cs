using System;
using System.Collections.Generic;

namespace PIU.Models;

public partial class DocenteCarreraEscuela
{
    public int Id { get; set; }

    public int? DocenteId { get; set; }

    public int? CarreraId { get; set; }

    public bool? Activo { get; set; }

    public virtual Carrera? Carrera { get; set; }

    public virtual Docente? Docente { get; set; }
}
