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

    public decimal ValorDescuento { get; set; }

    public decimal MontoMinimoCompra { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public int? UsoMaximo { get; set; }

    public int? UsoPorUsuario { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<UsosCupone> UsosCupones { get; set; } = new List<UsosCupone>();
}
