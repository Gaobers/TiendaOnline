using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PedidosController : Controller
    {
        // Usamos una lista estática para simular una base de datos temporal
        private static List<Pedido> _listaPedidos = new List<Pedido>
        {
            new Pedido { PedidoId = 1, NombreCliente = "Juan Pérez", EmailCliente = "juan@example.com", DireccionEntrega = "Calle Falsa 123", Total = 150.50m, FechaActualizacion = DateTime.Now },
            new Pedido { PedidoId = 2, NombreCliente = "María López", EmailCliente = "maria@example.com", DireccionEntrega = "Av. Siempre Viva 742", Total = 85.00m, FechaActualizacion = DateTime.Now.AddDays(-1) }
        };

        // Acción para ver la lista de pedidos
        public IActionResult Index()
        {
            // Retornamos la lista compartida, no una nueva cada vez
            return View(_listaPedidos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pedido pedido)
        {
            // 1. ESTA ES LA LÍNEA CLAVE: 
            // Le decimos a C# que ignore lo que venga del formulario sobre la fecha
            ModelState.Remove("FechaActualizacion");

            if (ModelState.IsValid)
            {
                // 2. Generamos el ID
                pedido.PedidoId = _listaPedidos.Count > 0 ? _listaPedidos.Max(p => p.PedidoId) + 1 : 1;

                // 3. ASIGNAMOS LA FECHA AQUÍ MANUALMENTE
                pedido.FechaActualizacion = DateTime.Now;

                // 4. Guardamos
                _listaPedidos.Add(pedido);

                return RedirectToAction(nameof(Index));
            }
            // Si hay error, regresamos a la vista
            return View(pedido);
        }

            // === BOTÓN EDITAR (VISTA) ===
        public IActionResult Edit(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }

        // === BOTÓN EDITAR (GUARDAR CAMBIOS) ===
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pedido pedidoActualizado)
        {
            ModelState.Remove("FechaActualizacion");
            var pedidoEnLista = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);

            if (pedidoEnLista != null && ModelState.IsValid)
            {
                pedidoEnLista.NombreCliente = pedidoActualizado.NombreCliente;
                pedidoEnLista.EmailCliente = pedidoActualizado.EmailCliente;
                pedidoEnLista.DireccionEntrega = pedidoActualizado.DireccionEntrega;
                pedidoEnLista.Total = pedidoActualizado.Total;
                pedidoEnLista.FechaActualizacion = DateTime.Now; // Se actualiza la fecha al editar

                return RedirectToAction(nameof(Index));
            }
            return View(pedidoActualizado);
        }

        // === BOTÓN ELIMINAR (VISTA DE CONFIRMACIÓN) ===
        public IActionResult Delete(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido == null) return NotFound();
            return View(pedido);
        }

        // === BOTÓN ELIMINAR (CONFIRMAR BORRADO) ===
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pedido = _listaPedidos.FirstOrDefault(p => p.PedidoId == id);
            if (pedido != null)
            {
                _listaPedidos.Remove(pedido);
            }
            return RedirectToAction(nameof(Index));
        }
    }
    }
