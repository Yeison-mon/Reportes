using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Contador
{
    public int Id { get; set; }

    public int Recepcion { get; set; }

    public int Ingreso { get; set; }

    public int Inspeccion { get; set; }

    public int Cotizacion { get; set; }

    public int Ordentrabajo { get; set; }
}
