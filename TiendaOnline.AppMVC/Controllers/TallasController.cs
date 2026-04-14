using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class TallasController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public TallasController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Tallas
        //Filtros
        public async Task<IActionResult> Index(string numero, byte? estatus)
        {
            var query = _context.Tallas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(numero))
                query = query.Where(c => c.Numero.Contains(numero));

            if (estatus.HasValue)
                query = query.Where(c => c.Estatus == estatus.Value);

            var categorias = await query
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            ViewBag.Estatus = estatus;
            ViewBag.Numero = numero;

            return View(categorias);
        }


        // GET: Tallas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talla = await _context.Tallas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talla == null)
            {
                return NotFound();
            }

            return View(talla);
        }

        // GET: Tallas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tallas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Estatus,FechaCreacion")] Talla talla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talla);
        }

        // GET: Tallas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talla = await _context.Tallas.FindAsync(id);
            if (talla == null)
            {
                return NotFound();
            }
            return View(talla);
        }

        // POST: Tallas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Estatus")] Talla talla)
        {
            if (id != talla.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(talla);
            }

            try
            {
                var tallaDb = await _context.Tallas.FindAsync(id);

                if (tallaDb == null)
                {
                    return NotFound();
                }

                tallaDb.Numero = talla.Numero;
                tallaDb.Estatus = talla.Estatus;

                // NO tocar FechaCreacion

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TallaExists(talla.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Tallas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talla = await _context.Tallas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (talla == null)
            {
                return NotFound();
            }

            return View(talla);
        }

        // POST: Tallas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talla = await _context.Tallas.FindAsync(id);
            if (talla != null)
            {
                _context.Tallas.Remove(talla);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TallaExists(int id)
        {
            return _context.Tallas.Any(e => e.Id == id);
        }
    }
}
