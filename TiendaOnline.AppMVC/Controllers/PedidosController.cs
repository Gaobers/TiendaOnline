using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PedidosController : Controller
    {
        // Acción para ver la lista de pedidos (Index)
        public IActionResult Index()
        {
            var listaPedidos = new List<Pedido>
            {
                new Pedido
                {
                    PedidoId = 1,
                    NombreCliente = "Juan Pérez",
                    EmailCliente = "juan@example.com",
                    DireccionEntrega = "Calle Falsa 123",
                    Total = 150.50m,
                    FechaActualizacion = DateTime.Now
                },
                new Pedido
                {
                    PedidoId = 2,
                    NombreCliente = "María López",
                    EmailCliente = "maria@example.com",
                    DireccionEntrega = "Av. Siempre Viva 742",
                    Total = 85.00m,
                    FechaActualizacion = DateTime.Now.AddDays(-1)
                }
            };

            return View(listaPedidos);
        }

        // Acción para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Acción para recibir los datos del formulario (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                // Aquí guardarías en la base de datos
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }
    }
}
