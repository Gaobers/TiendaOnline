using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Carrito
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<DetallesCarrito> DetallesCarritos { get; set; } = new List<DetallesCarrito>();

    public virtual Usuario Usuario { get; set; } = null!;
}
