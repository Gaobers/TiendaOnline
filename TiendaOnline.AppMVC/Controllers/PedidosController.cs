using Microsoft.AspNetCore.Mvc;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PedidosController : Controller
    {
        // 1. LISTA ESTÁTICA (Sintaxis simplificada 'new()' para evitar avisos)
        private static readonly List<Pedido> _listaPedidos = new()
        {
            new()

            {
                PedidoId = 1,
                NombreCliente = "Juan Pérez",
                EmailCliente = "juan@example.com",
                DireccionEntrega = "Calle Falsa 123",
                Total = 150.50m,
                Estado = "Pendiente",
                FechaActualizacion = DateTime.Now
            }
        };

        // 2. MÉTODOS (Todos dentro de la clase, pero fuera de la lista)

        public IActionResult Index() => View(_listaPedidos);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pedido pedido)
        {
            if (pedido.Total <= 0)
            {
                ModelState.AddModelError("Total", "El precio debe ser mayor a 0 dólares.");
            }

            if (ModelState.IsValid)
            {
                pedido.PedidoId = _listaPedidos.Count > 0 ? _listaPedidos.Max(p => p.PedidoId) + 1 : 1;
                pedido.FechaActualizacion = DateTime.Now;

                if (string.IsNullOrEmpty(pedido.Estado))
                {
                    pedido.Estado = "Nuevo";
                }

                _listaPedidos.Add(pedido);
                TempData["Mensaje"] = "Pedido guardado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public IActionResult Edit(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }

        public IActionResult Delete(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }
    } // Cierre de la Clase
} // Cierre del Namespace