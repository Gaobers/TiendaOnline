using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Marca
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la marca es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
    [Display(Name = "Nombre de la marca")]
    public string Nombre { get; set; } = null!;

    [MaxLength(300, ErrorMessage = "La descripción no puede exceder los 300 caracteres.")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }


    [Display(Name = "Estado")]
    public byte Estatus { get; set; }


    [DataType(DataType.DateTime)]
    [Display(Name = "Fecha de Creación")]
    public DateTime FechaCreacion { get; set; }


    [DataType(DataType.DateTime)]
    [Display(Name = "Ultima Actualización")]
    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
