using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Inventario
{
    public int InventarioId { get; set; }

    public int ProductoId { get; set; }

    public int TallaId { get; set; }

    public int Stock { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public virtual ICollection<AjusteInventario> AjusteInventarios { get; set; } = new List<AjusteInventario>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual Talla Talla { get; set; } = null!;
}
