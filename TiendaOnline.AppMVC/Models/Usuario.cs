using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Usuario
{
    public int Id { get; set; }


    [Required(ErrorMessage ="El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Nombre { get; set; } = null!;


    [Required(ErrorMessage = "El correo electrónico no puede estar vacío.")]
    [EmailAddress(ErrorMessage = "Debe ingresar un correo válido")]
    [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres")]
    public string Correo { get; set; } = null!;


    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(255, ErrorMessage = "La contraseña no puede superar los 255 caracteres")]
    public string PasswordHash { get; set; } = null!;


    public byte Estatus { get; set; }


    public DateTime FechaCreacion { get; set; }


    public DateTime? FechaActualizacion { get; set; }


    [Required(ErrorMessage = "El rol es obligatorio")]
    public int RolId { get; set; }

    [StringLength(50, ErrorMessage = "El campo no puede tener más de 50 caracteres")]
    [RegularExpression(@"^(?!.*(.)\1{4,}).*$", ErrorMessage = "No se permiten caracteres repetitivos excesivos")]
    public string? Apellido { get; set; }


    [Phone(ErrorMessage = "Caracteres no permitidos")]
    [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres")]
    public string? Telefono { get; set; }



    public virtual ICollection<AjustesInventario> AjustesInventarios { get; set; } = new List<AjustesInventario>();
    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
    public virtual ICollection<DireccionesUsuario> DireccionesUsuarios { get; set; } = new List<DireccionesUsuario>();
    public virtual ICollection<HistorialesEstadosPedido> HistorialesEstadosPedidos { get; set; } = new List<HistorialesEstadosPedido>();
    public virtual ICollection<Notificacione> Notificaciones { get; set; } = new List<Notificacione>();
    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    public virtual Role Rol { get; set; } = null!;
    public virtual ICollection<UsosCupone> UsosCupones { get; set; } = new List<UsosCupone>();
}
