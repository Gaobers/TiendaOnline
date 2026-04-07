using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaOnline.AppMVC.Models;

public partial class Pedido
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del Cliente es obligatorio")]
    [StringLength(100, MinimumLength = 5)]
    [Display(Name = "Nombre del Cliente")]
    public string NombreCliente { get; set; } = null!;


    [Required(ErrorMessage = "El correo electrónico es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    public string EmailCliente { get; set; } = null!;

    [Required(ErrorMessage = "La dirección de entrega es obligatoria")]
    public string DireccionEntrega { get; set; } = null!;

    [Required(ErrorMessage = "El total es obligatorio")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
    public decimal Total { get; set; }

    [Required(ErrorMessage = "La fecha de creación es obligatoria")]
    public DateTime FechaCreacion { get; set; }

    [Required(ErrorMessage = "El usuario es obligatorio")]
    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El método de envío es obligatorio")]
    public int MetodoEnvioId { get; set; }

    [Required(ErrorMessage = "El estado del pedido es obligatorio")]
    public int EstadoPedidoId { get; set; }

    [Required(ErrorMessage = "El subtotal es obligatorio")]
    public decimal SubTotal { get; set; }

    [Required(ErrorMessage = "El costo de envío es obligatorio")]
    public decimal CostoEnvio { get; set; }

    // --- Campos Opcionales (pueden ser nulos) ---

    public DateTime? FechaActualizacion { get; set; }

    public int? DireccionUsuarioId { get; set; }

    public int? CuponId { get; set; }

    public string? ApellidoCliente { get; set; }

    [Phone(ErrorMessage = "El formato del teléfono no es válido")]
    public string? TelefonoCliente { get; set; }
    public string? ReferenciaEntrega { get; set; }
    public decimal DescuentoTotal { get; set; }
    public string? Observaciones { get; set; }

    // --- Relaciones (Virtuales) ---

    public virtual Cupone? Cupon { get; set; }

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual DireccionesUsuario? DireccionUsuario { get; set; }

    public virtual EstadosPedido EstadoPedido { get; set; } = null!;

    public virtual ICollection<HistorialesEstadosPedido> HistorialesEstadosPedidos { get; set; } = new List<HistorialesEstadosPedido>();

    public virtual MetodosEnvio MetodoEnvio { get; set; } = null!;

    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    public virtual UsosCupone? UsosCupone { get; set; }    public virtual Usuario Usuario { get; set; } = null!;}
