using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class AjusteInventario
{
    public int AjusteInventarioId { get; set; }

    public int InventarioId { get; set; }

    public string Tipo { get; set; } = null!;

    public int Cantidad { get; set; }

    public string? Motivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Inventario Inventario { get; set; } = null!;
}
