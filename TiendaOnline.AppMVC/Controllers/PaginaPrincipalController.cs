
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        public IActionResult Index()
        {
            var model = new PaginaPrincipalViewModel();

            model.ProductosDestacados = new List<Producto>
            {
                new() { Id = 1, Nombre = "Nike Air Max", Precio = 120.00m, ImagenUrl = "/img/nike.jpg" },
                new() { Id = 2, Nombre = "Adidas Runner", Precio = 95.00m, ImagenUrl = "/img/adidas.jpg" }
            };

            model.PromocionesActivas = new List<Promocion>
            {
                new() { Id = 1, Icono = "fa-truck", Texto = "Envío gratis" },
                new() { Id = 2, Icono = "fa-tag", Texto = "20% descuento" }
            };

            return View(model);
        }
    }
}