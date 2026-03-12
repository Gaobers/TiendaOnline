using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization; // Necesario para la moneda
using System.Linq;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PedidosController : Controller
    {
        // Lista estática temporal
        private static List<Pedido> _listaPedidos = new List<Pedido>
        {
            new Pedido { PedidoId = 1, NumeroOrden = "ORD-001", NombreCliente = "Juan Pérez", EmailCliente = "juan@example.com", DireccionEntrega = "Calle Falsa 123", Total = 150.50m, Estado = "Completado", FechaRegistro = DateTime.Now, FechaActualizacion = DateTime.Now },
            new Pedido { PedidoId = 2, NumeroOrden = "ORD-002", NombreCliente = "María López", EmailCliente = "maria@example.com", DireccionEntrega = "Av. Siempre Viva 742", Total = 85.00m, Estado = "Pendiente", FechaRegistro = DateTime.Now.AddDays(-1), FechaActualizacion = DateTime.Now.AddDays(-1) }
        };

        // Forzamos la cultura de Estados Unidos para que el símbolo sea $
        private readonly CultureInfo _culturaDolar = new CultureInfo("en-US");

        public IActionResult Index()
        {
            // Pasamos la cultura a la vista mediante el hilo actual para asegurar el símbolo $
            System.Threading.Thread.CurrentThread.CurrentCulture = _culturaDolar;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _culturaDolar;

            return View(_listaPedidos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pedido pedido)
        {  ModelState.Remove("FechaActualizacion");
            ModelState.Remove("FechaRegistro");
            ModelState.Remove("NumeroOrden");
            ModelState.Remove("Estado");

            if (ModelState.IsValid)
            {     pedido.PedidoId = _listaPedidos.Count > 0 ? _listaPedidos.Max(p => p.PedidoId) + 1 : 1;
                pedido.NumeroOrden = "ORD-" + pedido.PedidoId.ToString("D3");
                pedido.FechaRegistro = DateTime.Now;
                pedido.FechaActualizacion = DateTime.Now;
                pedido.Estado = "Pendiente"; // Estado inicial
                _listaPedidos.Add(pedido);        return RedirectToAction(nameof(Index));
            }         return View(pedido);
        }             public IActionResult Edit(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
      }     [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pedido pedidoEditado)
        {
            var pedidoOriginal = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);

            // Quitamos la validación de fecha porque la generamos nosotros
            ModelState.Remove("FechaActualizacion");
            if (pedidoOriginal != null && ModelState.IsValid)
            {
                // ACTUALIZACIÓN DE DATOS
                pedidoOriginal.NombreCliente = pedidoEditado.NombreCliente;
                pedidoOriginal.EmailCliente = pedidoEditado.EmailCliente;
                pedidoOriginal.DireccionEntrega = pedidoEditado.DireccionEntrega;
                pedidoOriginal.Total = pedidoEditado.Total;

                // ACTUALIZACIÓN DE ESTADO (AQUÍ SE CAMBIA EL ESTADO)
                pedidoOriginal.Estado = pedidoEditado.Estado;

                pedidoOriginal.FechaActualizacion = DateTime.Now;

                return RedirectToAction(nameof(Index));
            }
            return View(pedidoEditado);
        }

        public IActionResult Details(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }

        // Método extra para cambiar estado rápidamente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarEstado(int id, string nuevoEstado)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido != null)
            {
                pedido.Estado = nuevoEstado;
                pedido.FechaActualizacion = DateTime.Now;
            }

            return RedirectToAction(nameof(Index));
        }      public IActionResult Delete(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }         [HttpPost, ActionName("Delete")]
                   [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido != null) _listaPedidos.Remove(pedido);
            return RedirectToAction(nameof(Index));
        }      public IActionResult Delete(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }         [HttpPost, ActionName("Delete")]
                   [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido != null) _listaPedidos.Remove(pedido);
            return RedirectToAction(nameof(Index));
        }
    }
}
