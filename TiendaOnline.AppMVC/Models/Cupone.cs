using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Cupone
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El codigo es obligatorio")]
    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El tipo de descuento es obligatorio")]
    public string TipoDescuento { get; set; } = null!;
    [Required(ErrorMessage = "El valor del descuento es obligatorio")]
    public decimal ValorDescuento { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "El monto mínimo no puede ser negativo")]
    public decimal MontoMinimoCompra { get; set; }
    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
    public DateTime FechaInicio { get; set; }
    [Required(ErrorMessage = "La fecha de fin es obligatoria")]
    public DateTime FechaFin { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "El uso máximo debe ser al menos 1")]
    public int? UsoMaximo { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "El uso por usuario debe ser al menos 1")]
    public int? UsoPorUsuario { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<UsosCupone> UsosCupones { get; set; } = new List<UsosCupone>();
}
