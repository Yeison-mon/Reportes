using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class FotosLaboratorio
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public int Nrocontrol { get; set; }

    public string Base64 { get; set; } = null!;

    public string Comprimido { get; set; } = null!;

    public int Nrofoto { get; set; }

    public int Documento { get; set; }

    public DateTime Fecha { get; set; }
}
