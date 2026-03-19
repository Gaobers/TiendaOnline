using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class UsosCupone
{
    public int Id { get; set; }

    public int CuponId { get; set; }

    public int UsuarioId { get; set; }

    public int PedidoId { get; set; }

    public decimal MontoDescuentoAplicado { get; set; }

    public DateTime FechaUso { get; set; }

    public virtual Cupone Cupon { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
