using System;
using System.Collections.Generic; // <--- AGREGA ESTA LÍNEA AHORA
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int TallaId { get; set; }
        public decimal SubTotal { get; set; }
        public string NumeroOrden { get; set; } = string.Empty;

        [Required]
        [DisplayName("Nombre del Cliente")]
        public string NombreCliente { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailCliente { get; set; } = string.Empty;

        public string? DireccionEntrega { get; set; }

        // ESTO ES LO QUE FALTABA PARA EL DBCONTEXT:
        [DisplayName("Estado del Pedido")]
        public string Estado { get; set; } = "PENDIENTE";

        [Required]
        public decimal Total { get; set; }

        [DisplayName("Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Fecha de Actualización")]
        public DateTime? FechaActualizacion { get; set; }
        // Esto crea el puente con los detalles del pedido
        public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
    }
}