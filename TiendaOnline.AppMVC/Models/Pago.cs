using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int PedidoId { get; set; }

    public int MetodoPagoId { get; set; }

    public decimal Monto { get; set; }

    public string EstadoPago { get; set; } = null!;

    public string? ReferenciaTransaccion { get; set; }

    public string? RespuestaPasarela { get; set; }

    public DateTime? FechaPago { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual MetodosPago MetodoPago { get; set; } = null!;

    public virtual Pedido Pedido { get; set; } = null!;
}
