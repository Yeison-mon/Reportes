using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Ordentrabajo
{
    public int Id { get; set; }

    public int Orden { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int Idusuario { get; set; }

    public int Idusuorden { get; set; }

    public string Observacion { get; set; } = null!;
}
