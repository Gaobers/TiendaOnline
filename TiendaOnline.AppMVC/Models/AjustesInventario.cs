using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class AjustesInventario
{
    public int Id { get; set; }

    public int InventarioId { get; set; }

    public int UsuarioId { get; set; }

    public string Tipo { get; set; } = null!;

    public int Cantidad { get; set; }

    public string? Motivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Inventario Inventario { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
