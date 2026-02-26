using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Talla
{
    public int TallaId { get; set; }

    public string Numero { get; set; } = null!;

    public byte Estatus { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
