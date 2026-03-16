using System;
using System.Collections.Generic;
namespace TiendaOnline.AppMVC.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int RolId { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<AjustesInventario> AjustesInventarios { get; set; } = new List<AjustesInventario>();

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DireccionesUsuario> DireccionesUsuarios { get; set; } = new List<DireccionesUsuario>();

    public virtual ICollection<HistorialesEstadosPedido> HistorialesEstadosPedidos { get; set; } = new List<HistorialesEstadosPedido>();

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Role Rol { get; set; } = null!;

    public virtual ICollection<UsosCupone> UsosCupones { get; set; } = new List<UsosCupone>();
}
