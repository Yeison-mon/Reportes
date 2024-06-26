using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Pais
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string Inicial { get; set; } = null!;

    public virtual ICollection<Departamentos> Departamentos { get; set; } = new List<Departamentos>();
}
