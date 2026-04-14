using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class MetodosPago
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = null!;


    [Display(Name = "Descripción")]
    public string? Descripcion { get; set; }

    [Display(Name = "Estado")]
    public byte Estatus { get; set; }

    [Display(Name = "Fecha de creación")]
    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
