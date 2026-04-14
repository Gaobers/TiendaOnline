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
        public async Task<IActionResult> Index(string nombre, int? estadoPedidoId)
        {
            var query = _context.Pedidos
                .Include(p => p.EstadoPedido)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(p => p.NombreCliente.Contains(nombre));

            if (estadoPedidoId.HasValue)
                query = query.Where(p => p.EstadoPedidoId == estadoPedidoId.Value);

            var pedidos = await query
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            ViewBag.EstadosPedido = await _context.EstadosPedidos
                .Where(e => e.Estatus == 1)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre,
                    Selected = estadoPedidoId.HasValue && e.Id == estadoPedidoId.Value
                })
                .ToListAsync();

            ViewBag.Nombre = nombre;
            ViewBag.EstadoPedidoId = estadoPedidoId;

            return View(pedidos);
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Cupon)
                .Include(p => p.DireccionUsuario)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoEnvio)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            CargarCombos();
            return View();
        }

        // POST: Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreCliente,EmailCliente,DireccionEntrega,Total,UsuarioId,DireccionUsuarioId,MetodoEnvioId,EstadoPedidoId,CuponId,ApellidoCliente,TelefonoCliente,ReferenciaEntrega,SubTotal,DescuentoTotal,CostoEnvio,Observaciones")] Pedido pedido)
        {
            ModelState.Remove("Usuario");
            ModelState.Remove("MetodoEnvio");
            ModelState.Remove("EstadoPedido");
            ModelState.Remove("Cupon");
            ModelState.Remove("DireccionUsuario");
            ModelState.Remove("DetallesPedidos");
            ModelState.Remove("HistorialesEstadosPedidos");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Pagos");
            ModelState.Remove("UsosCupone");

            pedido.FechaCreacion = DateTime.Now;
            pedido.FechaActualizacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var errores = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => $"{x.Key}: {string.Join(" | ", x.Value.Errors.Select(e => e.ErrorMessage))}")
                .ToList();

            foreach (var error in errores)
            {
                Console.WriteLine(error);
            }

            CargarCombos(pedido);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound();

            CargarCombos(pedido);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCliente,EmailCliente,DireccionEntrega,Total,UsuarioId,DireccionUsuarioId,MetodoEnvioId,EstadoPedidoId,CuponId,ApellidoCliente,TelefonoCliente,ReferenciaEntrega,SubTotal,DescuentoTotal,CostoEnvio,Observaciones")] Pedido pedido)
        {
            if (id != pedido.Id)
                return NotFound();

            ModelState.Remove("Usuario");
            ModelState.Remove("MetodoEnvio");
            ModelState.Remove("EstadoPedido");
            ModelState.Remove("Cupon");
            ModelState.Remove("DireccionUsuario");
            ModelState.Remove("DetallesPedidos");
            ModelState.Remove("HistorialesEstadosPedidos");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Pagos");
            ModelState.Remove("UsosCupone");

            if (ModelState.IsValid)
            {
                try
                {
                    var pedidoDb = await _context.Pedidos.FindAsync(id);

                    if (pedidoDb == null)
                        return NotFound();

                    pedidoDb.NombreCliente = pedido.NombreCliente;
                    pedidoDb.ApellidoCliente = pedido.ApellidoCliente;
                    pedidoDb.EmailCliente = pedido.EmailCliente;
                    pedidoDb.TelefonoCliente = pedido.TelefonoCliente;
                    pedidoDb.DireccionEntrega = pedido.DireccionEntrega;
                    pedidoDb.ReferenciaEntrega = pedido.ReferenciaEntrega;
                    pedidoDb.UsuarioId = pedido.UsuarioId;
                    pedidoDb.DireccionUsuarioId = pedido.DireccionUsuarioId;
                    pedidoDb.MetodoEnvioId = pedido.MetodoEnvioId;
                    pedidoDb.EstadoPedidoId = pedido.EstadoPedidoId;
                    pedidoDb.CuponId = pedido.CuponId;
                    pedidoDb.SubTotal = pedido.SubTotal;
                    pedidoDb.DescuentoTotal = pedido.DescuentoTotal;
                    pedidoDb.CostoEnvio = pedido.CostoEnvio;
                    pedidoDb.Total = pedido.Total;
                    pedidoDb.Observaciones = pedido.Observaciones;

                    // NO tocar FechaCreacion
                    pedidoDb.FechaActualizacion = DateTime.Now;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                        return NotFound();

                    throw;
                }
            }

            var errores = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => $"{x.Key}: {string.Join(" | ", x.Value.Errors.Select(e => e.ErrorMessage))}")
                .ToList();

            foreach (var error in errores)
            {
                Console.WriteLine(error);
            }

            CargarCombos(pedido);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = await _context.Pedidos
                .Include(p => p.Cupon)
                .Include(p => p.DireccionUsuario)
                .Include(p => p.EstadoPedido)
                .Include(p => p.MetodoEnvio)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido != null)
                _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void CargarCombos(Pedido? pedido = null)
        {
            ViewData["CuponId"] = new SelectList(
                _context.Cupones,
                "Id",
                "Codigo",
                pedido?.CuponId
            );

            ViewData["DireccionUsuarioId"] = new SelectList(
                _context.DireccionesUsuarios,
                "Id",
                "DireccionExacta",
                pedido?.DireccionUsuarioId
            );

            ViewData["EstadoPedidoId"] = new SelectList(
                _context.EstadosPedidos,
                "Id",
                "Nombre",
                pedido?.EstadoPedidoId
            );

            ViewData["MetodoEnvioId"] = new SelectList(
                _context.MetodosEnvios,
                "Id",
                "Nombre",
                pedido?.MetodoEnvioId
            );

            ViewData["UsuarioId"] = new SelectList(
                _context.Usuarios
                    .Select(u => new
                    {
                        u.Id,
                        NombreCompleto = u.Nombre + " " + u.Apellido
                    })
                    .ToList(),
                "Id",
                "NombreCompleto",
                pedido?.UsuarioId
            );
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}