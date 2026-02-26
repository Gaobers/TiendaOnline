using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class PedidoDetalle
{
    public int PedidoDetalleId { get; set; }

    public int PedidoId { get; set; }

    public int ProductoId { get; set; }

    public int TallaId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal SubTotal { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;

    public virtual Talla Talla { get; set; } = null!;
}
