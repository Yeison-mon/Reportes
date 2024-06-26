using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class RecepcionDetalle
{
    public int Id { get; set; }

    public int Ingreso { get; set; }

    public int Idrecepcion { get; set; }

    public int Idequipo { get; set; }

    public int Idmarca { get; set; }

    public int Idmodelo { get; set; }

    public string Observacion { get; set; } = null!;

    public int Fotos { get; set; }

    public string? Serie { get; set; }

    public string? Codigo { get; set; }

    public DateTime Fechaing { get; set; }

    public int Idusuario { get; set; }

    public int Idintervalo1 { get; set; }

    public int Idintervalo2 { get; set; }

    public int Idmagnitud { get; set; }

    public int Idcliente { get; set; }

    public string Ubicacion { get; set; } = null!;

    public bool Material { get; set; }

    public string Accesorios { get; set; } = null!;

    public int Cantidad { get; set; }

    public int Idtipoindicacion { get; set; }

    public string TipoServicio { get; set; } = null!;

    public int Renglon { get; set; }

    public bool ConSerie { get; set; }

    public int Cotizado { get; set; }

    public string Estado { get; set; } = null!;

    public int Inactivo { get; set; }
}
