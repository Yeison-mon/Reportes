using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Ciudad
{
    public int Id { get; set; }

    public int Iddepartamento { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public virtual Departamentos IddepartamentoNavigation { get; set; } = null!;
}
