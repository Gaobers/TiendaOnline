using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class ProductoImagen
{
    public int ProductoImagenId { get; set; }

    public int ProductoId { get; set; }

    public string Url { get; set; } = null!;

    public bool EsPrincipal { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Producto Producto { get; set; } = null!;
}
