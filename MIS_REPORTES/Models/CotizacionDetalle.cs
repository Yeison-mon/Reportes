using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class CotizacionDetalle
{
    public int Id { get; set; }

    public int Idcotizacion { get; set; }

    public int Idcliente { get; set; }

    public int Idingreso { get; set; }

    public int Idequipo { get; set; }

    public int Idmodelo { get; set; }

    public int Idintervalo1 { get; set; }

    public int Idservicio { get; set; }

    public int Idsubservicio { get; set; }

    public decimal Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Descuento { get; set; }

    public int Idmoneda { get; set; }

    public decimal Iva { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Observacion { get; set; } = null!;

    public string Codigo { get; set; } = null!;

    public string Serie { get; set; } = null!;

    public int Idintervalo2 { get; set; }
}
