using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class PedidosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public PedidosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var tiendaOnlineZapContext = _context.Pedidos.Include(p => p.Cupon).Include(p => p.DireccionUsuario).Include(p => p.EstadoPedido).Include(p => p.MetodoEnvio).Include(p => p.Usuario);
            return View(await tiendaOnlineZapContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cupon)
                .Include(p => p.DireccionUsuario)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoEnvio)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["CuponId"] = new SelectList(_context.Cupones, "Id", "Codigo");
            ViewData["DireccionUsuarioId"] = new SelectList(_context.DireccionesUsuarios, "Id", "DireccionExacta");
            ViewData["EstadoPedidoId"] = new SelectList(_context.EstadosPedidos, "Id", "Nombre");
            ViewData["MetodoEnvioId"] = new SelectList(_context.MetodosEnvios, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCliente,EmailCliente,DireccionEntrega,Total,FechaCreacion,FechaActualizacion,UsuarioId,DireccionUsuarioId,MetodoEnvioId,EstadoPedidoId,CuponId,ApellidoCliente,TelefonoCliente,ReferenciaEntrega,SubTotal,DescuentoTotal,CostoEnvio,Observaciones")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CuponId"] = new SelectList(_context.Cupones, "Id", "Id", pedido.CuponId);
            ViewData["DireccionUsuarioId"] = new SelectList(_context.DireccionesUsuarios, "Id", "Id", pedido.DireccionUsuarioId);
            ViewData["EstadoPedidoId"] = new SelectList(_context.EstadosPedidos, "Id", "Id", pedido.EstadoPedidoId);
            ViewData["MetodoEnvioId"] = new SelectList(_context.MetodosEnvios, "Id", "Id", pedido.MetodoEnvioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["CuponId"] = new SelectList(_context.Cupones, "Id", "Id", pedido.CuponId);
            ViewData["DireccionUsuarioId"] = new SelectList(_context.DireccionesUsuarios, "Id", "Id", pedido.DireccionUsuarioId);
            ViewData["EstadoPedidoId"] = new SelectList(_context.EstadosPedidos, "Id", "Id", pedido.EstadoPedidoId);
            ViewData["MetodoEnvioId"] = new SelectList(_context.MetodosEnvios, "Id", "Id", pedido.MetodoEnvioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCliente,EmailCliente,DireccionEntrega,Total,FechaCreacion,FechaActualizacion,UsuarioId,DireccionUsuarioId,MetodoEnvioId,EstadoPedidoId,CuponId,ApellidoCliente,TelefonoCliente,ReferenciaEntrega,SubTotal,DescuentoTotal,CostoEnvio,Observaciones")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CuponId"] = new SelectList(_context.Cupones, "Id", "Id", pedido.CuponId);
            ViewData["DireccionUsuarioId"] = new SelectList(_context.DireccionesUsuarios, "Id", "Id", pedido.DireccionUsuarioId);
            ViewData["EstadoPedidoId"] = new SelectList(_context.EstadosPedidos, "Id", "Id", pedido.EstadoPedidoId);
            ViewData["MetodoEnvioId"] = new SelectList(_context.MetodosEnvios, "Id", "Id", pedido.MetodoEnvioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cupon)
                .Include(p => p.DireccionUsuario)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoEnvio)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
