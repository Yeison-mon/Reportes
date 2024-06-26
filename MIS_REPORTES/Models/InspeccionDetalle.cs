using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class InspeccionDetalle
{
    public int Id { get; set; }

    public int Idinspeccion { get; set; }

    public int Idingreso { get; set; }

    public int Idmaterial { get; set; }

    public string Piezas { get; set; } = null!;

    public string Funcionalidades { get; set; } = null!;

    public string Acabado { get; set; } = null!;

    public bool Estado { get; set; }

    public string Observacion { get; set; } = null!;
}
