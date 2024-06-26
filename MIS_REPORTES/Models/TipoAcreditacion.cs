using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class TipoAcreditacion
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public int Estado { get; set; }

    public string Acreditado { get; set; } = null!;

    public int Mantenimiento { get; set; }
}
