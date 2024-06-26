using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Modelos
{
    public int Id { get; set; }

    public int Idmarca { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }
}
