using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Clientes
{
    public int Id { get; set; }

    public string Documento { get; set; } = null!;

    public string Nombrecompleto { get; set; } = null!;

    public int Idciudad { get; set; }

    public string CorreoPrincipal { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public int Estado { get; set; }

    public string CorreoFactura { get; set; } = null!;

    public int Habitual { get; set; }

    public int Convenio { get; set; }
}
