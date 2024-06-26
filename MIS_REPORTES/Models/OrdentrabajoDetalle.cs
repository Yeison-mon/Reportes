using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class OrdentrabajoDetalle
{
    public int Id { get; set; }

    public int Idorden { get; set; }

    public int Idingreso { get; set; }

    public string Observacion { get; set; } = null!;
}
