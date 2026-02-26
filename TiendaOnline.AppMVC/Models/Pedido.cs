using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string EmailCliente { get; set; } = null!;

    public string DireccionEntrega { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public decimal Total { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
