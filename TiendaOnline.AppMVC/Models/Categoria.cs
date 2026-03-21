using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
    [Display(Name = "Nombre de la categoría")]
    public string Nombre { get; set; } = null!;


    [StringLength(250, MinimumLength = 3, ErrorMessage = "La descripción debe tener entre 3 y 250 caracteres.")]
    public string? Descripcion { get; set; }


    [Display(Name = "Estado")]
    public byte Estatus { get; set; }


    [Display(Name = "Fecha de Creación")]
    public DateTime FechaCreacion { get; set; }


    [Display(Name = "Fecha de Actualización")]
    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
