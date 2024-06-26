using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class MaterialDetalle
{
    public int Id { get; set; }

    public int Idrecepcion { get; set; }

    public int Idcliente { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Ingreso { get; set; }

    public string Serie { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public DateTime Fechaing { get; set; }
}
