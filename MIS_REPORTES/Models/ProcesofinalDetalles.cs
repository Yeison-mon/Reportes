using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class ProcesofinalDetalles
{
    public int Id { get; set; }

    public int Idprocesofinal { get; set; }

    public int Idingreso { get; set; }

    public string Lavado { get; set; } = null!;

    public string Componentes { get; set; } = null!;

    public string Ensamblaje { get; set; } = null!;

    public int Certificado { get; set; }

    public string Observacion { get; set; } = null!;

    public string Desarme { get; set; } = null!;

    public int Idacreditado { get; set; }

    public DateTime Fecha { get; set; }

    public string FechaCertificado { get; set; } = null!;
}
