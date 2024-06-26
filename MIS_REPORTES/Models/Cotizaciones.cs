using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Cotizaciones
{
    public int Id { get; set; }

    public int Cotizacion { get; set; }

    public string Anio { get; set; } = null!;

    public int Idcliente { get; set; }

    public int Idsede { get; set; }

    public int Idcontacto { get; set; }

    public DateTime Fecha { get; set; }

    public int Idusuario { get; set; }

    public string Estado { get; set; } = null!;

    public string Observacion { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal Descuento { get; set; }

    public decimal Gravable { get; set; }

    public decimal Exento { get; set; }

    public decimal Iva { get; set; }

    public decimal Reteiva { get; set; }

    public decimal Retefuente { get; set; }

    public decimal Reteica { get; set; }

    public decimal Total { get; set; }

    public int Aprobada { get; set; }

    public DateTime? Fechaaprobacion { get; set; }

    public DateTime? Fecenviada { get; set; }

    public string Correoenviado { get; set; } = null!;

    public int Rechazada { get; set; }

    public DateTime? Fecharechazada { get; set; }

    public string Obserenvio { get; set; } = null!;

    public DateTime? Fechaanula { get; set; }

    public int Idusuarioanula { get; set; }

    public string Observacionanula { get; set; } = null!;

    public string Validez { get; set; } = null!;

    public decimal Trm { get; set; }

    public int Idusuenviada { get; set; }
}
