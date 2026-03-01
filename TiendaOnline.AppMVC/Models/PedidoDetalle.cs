using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaOnline.AppMVC.Models
{
    public class PedidoDetalle
    {
        [Key]
        public int PedidoDetalleId { get; set; }

        public int PedidoId { get; set; }
        public int TallaId { get; set; } // Solo una vez
        public int ProductoId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        public virtual Pedido? Pedido { get; set; }
        public virtual Talla? Talla { get; set; }
    }
}

