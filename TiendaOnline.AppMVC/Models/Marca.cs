using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Marca
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
