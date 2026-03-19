using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Notificacione
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int? PedidoId { get; set; }

    public string Titulo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public bool EsLeida { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Pedido? Pedido { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
