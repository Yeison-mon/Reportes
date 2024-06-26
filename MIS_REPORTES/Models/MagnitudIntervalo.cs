using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class MagnitudIntervalo
{
    public int Id { get; set; }

    public int Idmagnitud { get; set; }

    public string? Desde { get; set; }

    public string? Hasta { get; set; }

    public int Estado { get; set; }

    public string Medida { get; set; } = null!;
}
