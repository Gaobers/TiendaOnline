using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class MetodosPagosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public MetodosPagosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: MetodosPagos
        //Filtros
        public async Task<IActionResult> Index(string nombre, byte? estatus)
        {
            var query = _context.MetodosPagos.AsQueryable();

            //Nombre
            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(c => c.Nombre.Contains(nombre));
            //Estado
            if (estatus.HasValue)
                query = query.Where(c => c.Estatus == estatus.Value);

            var metodosPagos = await query
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(metodosPagos);
        }

        // GET: MetodosPagos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosPago = await _context.MetodosPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodosPago == null)
            {
                return NotFound();
            }

            return View(metodosPago);
        }

        // GET: MetodosPagos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodosPagos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estatus,FechaCreacion")] MetodosPago metodosPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodosPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodosPago);
        }

        // GET: MetodosPagos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosPago = await _context.MetodosPagos.FindAsync(id);
            if (metodosPago == null)
            {
                return NotFound();
            }
            return View(metodosPago);
        }

        // POST: MetodosPagos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estatus")] MetodosPago metodosPago)
        {
            if (id != metodosPago.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(metodosPago);
            }

            try
            {
                var metodoPagoDb = await _context.MetodosPagos.FindAsync(id);

                if (metodoPagoDb == null)
                {
                    return NotFound();
                }

                metodoPagoDb.Nombre = metodosPago.Nombre;
                metodoPagoDb.Descripcion = metodosPago.Descripcion;
                metodoPagoDb.Estatus = metodosPago.Estatus;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetodosPagoExists(metodosPago.Id))
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

        // GET: MetodosPagos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosPago = await _context.MetodosPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodosPago == null)
            {
                return NotFound();
            }

            return View(metodosPago);
        }

        // POST: MetodosPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodosPago = await _context.MetodosPagos.FindAsync(id);
            if (metodosPago != null)
            {
                _context.MetodosPagos.Remove(metodosPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodosPagoExists(int id)
        {
            return _context.MetodosPagos.Any(e => e.Id == id);
        }
    }
}
