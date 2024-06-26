using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Inspecciones
{
    public int Id { get; set; }

    public int Inspeccion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int Idusuario { get; set; }

    public string Observacion { get; set; } = null!;
}
