using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Talla
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La talla es obligatoria.")]
    [StringLength(10, MinimumLength = 1, ErrorMessage = "La talla debe tener entre 1 y 10 caracteres.")]
    [Display(Name = "Talla")]
    public string Numero { get; set; } = null!;


    [Display(Name = "Estado")]
    public byte Estatus { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = "Fecha de Creación")]
    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
