using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public string NombreCliente { get; set; } = null!;


    public string EmailCliente { get; set; } = null!;

    public string DireccionEntrega { get; set; } = null!;

    public decimal Total { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int UsuarioId { get; set; }

    public int? DireccionUsuarioId { get; set; }

    public int MetodoEnvioId { get; set; }

    public int EstadoPedidoId { get; set; }

    public int? CuponId { get; set; }

    public string? ApellidoCliente { get; set; }

    public string? TelefonoCliente { get; set; }

    public string? ReferenciaEntrega { get; set; }

    public decimal SubTotal { get; set; }

    public decimal DescuentoTotal { get; set; }

    public decimal CostoEnvio { get; set; }

    public string? Observaciones { get; set; }

    public virtual Cupone? Cupon { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual DireccionesUsuario? DireccionUsuario { get; set; }

    public virtual EstadosPedido EstadoPedido { get; set; } = null!;

    public virtual ICollection<HistorialesEstadosPedido> HistorialesEstadosPedidos { get; set; } = new List<HistorialesEstadosPedido>();

    public virtual MetodosEnvio MetodoEnvio { get; set; } = null!;

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual UsosCupone? UsosCupone { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
