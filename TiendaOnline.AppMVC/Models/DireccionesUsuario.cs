using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class DireccionesUsuario
{
    public int Id { get; set; }
    [Display(Name = "Usuario")]
    public int UsuarioId { get; set; }

    public string? Alias { get; set; }
    [Required(ErrorMessage = "El departamento es obligatorio")]
    public string Departamento { get; set; } = null!;
    [Required(ErrorMessage = "El municipio es obligatorio")]
    public string Municipio { get; set; } = null!;
    [Required(ErrorMessage = "La dirección exacta es obligatoria")]
    [Display(Name = "Dirección Exacta")]
    public string DireccionExacta { get; set; } = null!;

    public string? Referencia { get; set; }
    [Display(Name = "Teléfono de Contacto")]
    public string? TelefonoContacto { get; set; }
    [Display(Name = "Dirección principal")]
    public bool EsPrincipal { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual Usuario Usuario { get; set; } = null!;
}
