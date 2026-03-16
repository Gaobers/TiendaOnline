using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public int TallaId { get; set; }

    public int Stock { get; set; }

    public int StockMinimo { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual ICollection<AjustesInventario> AjustesInventarios { get; set; } = new List<AjustesInventario>();

    public virtual ICollection<DetallesCarrito> DetallesCarritos { get; set; } = new List<DetallesCarrito>();

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual Talla Talla { get; set; } = null!;
}
