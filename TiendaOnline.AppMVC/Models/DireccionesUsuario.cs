using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class DireccionesUsuario
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string? Alias { get; set; }

    public string Departamento { get; set; } = null!;

    public string Municipio { get; set; } = null!;

    public string DireccionExacta { get; set; } = null!;

    public string? Referencia { get; set; }

    public string? TelefonoContacto { get; set; }

    public bool EsPrincipal { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Usuario Usuario { get; set; } = null!;
}
