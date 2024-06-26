using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Iva
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Valor { get; set; }

    public int Estado { get; set; }
}
