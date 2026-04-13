
using System.Collections.Generic;
using TiendaOnline.AppMVC.ViewModels;

namespace TiendaOnline.AppMVC.Models
{
    public class PaginaPrincipalViewModel
    {
        public List<Producto> ProductosDestacados { get; set; } = new();
        public List<Promocion> PromocionesActivas { get; set; } = new();
    }
}