
using Microsoft.AspNetCore.Mvc;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public PaginaPrincipalController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new PaginaPrincipalViewModel
            {
                ProductosDestacados = _context.Productos.ToList(),
                PromocionesActivas = new List<Promocion>
            {
                new() { Id = 1, Icono = "fa-truck", Texto = "Envío gratis" },
                new() { Id = 2, Icono = "fa-tag", Texto = "20% descuento" }
            }
            };

            return View(model);
        }
    }
}