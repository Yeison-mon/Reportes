using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class TipoIndicacion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }
}
