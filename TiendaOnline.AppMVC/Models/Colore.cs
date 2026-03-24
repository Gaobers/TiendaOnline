using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Colore
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre del color es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
    [RegularExpression(@"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
    public string Nombre { get; set; } = null!;

    [Display(Name = "Código Hexadecimal")]
    public string? CodigoHex { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<ProductosColore> ProductosColores { get; set; } = new List<ProductosColore>();
}
