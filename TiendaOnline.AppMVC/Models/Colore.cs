using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Colore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? CodigoHex { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<ProductosColore> ProductosColores { get; set; } = new List<ProductosColore>();
}
