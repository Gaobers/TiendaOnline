using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class DetallesCarrito
{
    public int Id { get; set; }

    public int CarritoId { get; set; }

    public int InventarioId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubTotal { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Carrito Carrito { get; set; } = null!;

    public virtual Inventario Inventario { get; set; } = null!;
}
