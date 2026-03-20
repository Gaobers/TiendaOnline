using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class HistorialesEstadosPedido
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int EstadoPedidoId { get; set; }

    public int? UsuarioId { get; set; }

    public string? Comentario { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual EstadosPedido EstadoPedido { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Usuario? Usuario { get; set; }
}
