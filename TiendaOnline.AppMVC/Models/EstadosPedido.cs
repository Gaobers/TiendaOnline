using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class EstadosPedido
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<HistorialesEstadosPedido> HistorialesEstadosPedidos { get; set; } = new List<HistorialesEstadosPedido>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
