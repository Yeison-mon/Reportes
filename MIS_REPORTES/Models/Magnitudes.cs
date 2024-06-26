using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Magnitudes
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }

    public string? Abreviatura { get; set; }

    public int Graficar { get; set; }

    public decimal DesdeAcreditado { get; set; }

    public decimal HastaAcreditado { get; set; }

    public string Color { get; set; } = null!;

    public decimal Meta { get; set; }

    public decimal MetaEquipos { get; set; }
}
