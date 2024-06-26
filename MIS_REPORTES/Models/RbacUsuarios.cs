using System;
using System.Collections.Generic;

namespace MIS_REPORTES.Models;

public partial class RbacUsuarios
{
    public int Id { get; set; }

    public string Nomusu { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string Nombrecompleto { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public string Email { get; set; } = null!; 

    public string Ip { get; set; } = null!;

    public string Mac { get; set; } = null!;
}
