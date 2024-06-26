using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class ContactosCliente
{
    public int Id { get; set; }

    public int Idcliente { get; set; }

    public int Idsede { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefonos { get; set; } = null!;

    public string Cargo { get; set; } = null!;
}
