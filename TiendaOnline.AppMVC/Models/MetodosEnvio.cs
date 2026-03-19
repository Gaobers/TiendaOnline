using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class MetodosEnvio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Costo { get; set; }

    public string? TiempoEstimado { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
