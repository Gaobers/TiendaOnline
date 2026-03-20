using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class DetallesPedido
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int InventarioId { get; set; }

    public string NombreProducto { get; set; } = null!;

    public string TallaTexto { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public int Cantidad { get; set; }

    public decimal SubTotal { get; set; }

    public virtual Inventario Inventario { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}
