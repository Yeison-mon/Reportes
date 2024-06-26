using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class CotizacionAprobacion
{
    public int Id { get; set; }

    public int Idcotizacion { get; set; }

    public string Contacto { get; set; } = null!;

    public string Cedula { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public int Estado { get; set; }

    public string? Tiempo { get; set; }

    public int Idusuario { get; set; }

    public int Tipo { get; set; }

    public string Conversacion { get; set; } = null!;
}
