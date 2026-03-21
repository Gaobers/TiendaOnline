using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Inventario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El producto es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar un producto válido.")]
    [Display(Name = "Producto")]
    public int ProductoId { get; set; }


    [Required(ErrorMessage = "La talla es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una talla válida.")]
    [Display(Name = "Talla")]
    public int TallaId { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    [Display(Name = "Stock actual")]
    public int Stock { get; set; }


    [Required(ErrorMessage = "El stock mínimo es obligatorio.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
    [Display(Name = "Stock mínimo")]
    public int StockMinimo { get; set; }

    [Display(Name = "Fecha de Actualización")]

    public DateTime FechaActualizacion { get; set; }

    public virtual ICollection<AjustesInventario> AjustesInventarios { get; set; } = new List<AjustesInventario>();

    public virtual ICollection<DetallesCarrito> DetallesCarritos { get; set; } = new List<DetallesCarrito>();

    public virtual ICollection<DetallesPedido> DetallesPedidos { get; set; } = new List<DetallesPedido>();

    public virtual Producto Producto { get; set; } = null!;

    public virtual Talla Talla { get; set; } = null!;
}
