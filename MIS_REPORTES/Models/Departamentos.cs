using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Departamentos
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Idpais { get; set; }

    public string? Codigo { get; set; }

    public virtual ICollection<Ciudad> Ciudads { get; set; } = new List<Ciudad>();

    public virtual Pais IdpaisNavigation { get; set; } = null!;
}
