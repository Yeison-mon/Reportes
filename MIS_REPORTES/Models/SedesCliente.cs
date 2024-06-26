using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class SedesCliente
{
    public int Id { get; set; }

    public int Idcliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public int Idciudad { get; set; }

    public string ContactoFactura { get; set; } = null!;

    public string CorreoFactura { get; set; } = null!;

    public string TelefonoFactura { get; set; } = null!;

    public string CopiaCorreoFactura { get; set; } = null!;
}
