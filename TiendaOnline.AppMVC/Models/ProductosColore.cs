using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class ProductosColore
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int ColorId { get; set; }

    public virtual Colore Color { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
