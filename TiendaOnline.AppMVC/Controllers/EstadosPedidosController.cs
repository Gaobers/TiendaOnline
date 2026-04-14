using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class EstadosPedidosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public EstadosPedidosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: EstadosPedidos
        //Filtros
        public async Task<IActionResult> Index(string nombre, byte? estatus)
        {
            var query = _context.EstadosPedidos.AsQueryable();

            //Nombre
            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(c => c.Nombre.Contains(nombre));
            //Estado
            if (estatus.HasValue)
                query = query.Where(c => c.Estatus == estatus);

            var estadosPedidos = await query
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(estadosPedidos);
        }

        // GET: EstadosPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosPedido = await _context.EstadosPedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadosPedido == null)
            {
                return NotFound();
            }

            return View(estadosPedido);
        }

        // GET: EstadosPedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadosPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estatus,FechaCreacion")] EstadosPedido estadosPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadosPedido);
        }

        // GET: EstadosPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosPedido = await _context.EstadosPedidos.FindAsync(id);
            if (estadosPedido == null)
            {
                return NotFound();
            }
            return View(estadosPedido);
        }

        // POST: EstadosPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estatus")] EstadosPedido estadosPedido)
        {
            if (id != estadosPedido.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(estadosPedido);
            }

            try
            {
                var estadoDb = await _context.EstadosPedidos.FindAsync(id);

                if (estadoDb == null)
                {
                    return NotFound();
                }

                estadoDb.Nombre = estadosPedido.Nombre;
                estadoDb.Descripcion = estadosPedido.Descripcion;
                estadoDb.Estatus = estadosPedido.Estatus;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadosPedidoExists(estadosPedido.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: EstadosPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadosPedido = await _context.EstadosPedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadosPedido == null)
            {
                return NotFound();
            }

            return View(estadosPedido);
        }

        // POST: EstadosPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosPedido = await _context.EstadosPedidos.FindAsync(id);
            if (estadosPedido != null)
            {
                _context.EstadosPedidos.Remove(estadosPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadosPedidoExists(int id)
        {
            return _context.EstadosPedidos.Any(e => e.Id == id);
        }
    }
}
