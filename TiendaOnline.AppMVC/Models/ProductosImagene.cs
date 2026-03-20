using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class ProductosImagene
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string Url { get; set; } = null!;

    public bool EsPrincipal { get; set; }

    public DateTime FechaCreacion { get; set; }

    public int Orden { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
