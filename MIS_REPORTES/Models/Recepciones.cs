using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class Recepciones
{
    public int Id { get; set; }

    public int Recepcion { get; set; }

    public int Idcliente { get; set; }

    public int Idsede { get; set; }

    public int Idcontacto { get; set; }

    public DateTime Fecha { get; set; }

    public int Idusuario { get; set; }

    public string Anio { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Observacion { get; set; } = null!;

    public int IdformaLlegada { get; set; }

    public string Talonario { get; set; } = null!;
}
