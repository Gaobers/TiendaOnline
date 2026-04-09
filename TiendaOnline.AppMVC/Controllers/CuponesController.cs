using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class CuponesController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public CuponesController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Cupones
        public async Task<IActionResult> Index(string codigo, byte? estatus, int top = 10)
        {
            var query = _context.Cupones.AsQueryable();

            //Codigo
            if (!string.IsNullOrWhiteSpace(codigo))
                query = query.Where(c => c.Codigo.Contains(codigo));
            //Estado
            if (estatus.HasValue)
                query = query.Where(c => c.Estatus == estatus.Value);
            //Top
            query = query.Take(top);

            var cupones = await query.ToListAsync();
            return View(cupones);
        }

        // GET: Cupones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupone = await _context.Cupones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupone == null)
            {
                return NotFound();
            }

            return View(cupone);
        }

        // GET: Cupones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cupones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Codigo,Descripcion,TipoDescuento,ValorDescuento,MontoMinimoCompra,FechaInicio,FechaFin,UsoMaximo,UsoPorUsuario,Estatus,FechaCreacion")] Cupone cupone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cupone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cupone);
        }

        // GET: Cupones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupone = await _context.Cupones.FindAsync(id);
            if (cupone == null)
            {
                return NotFound();
            }
            return View(cupone);
        }

        // POST: Cupones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Codigo,Descripcion,TipoDescuento,ValorDescuento,MontoMinimoCompra,FechaInicio,FechaFin,UsoMaximo,UsoPorUsuario,Estatus,FechaCreacion")] Cupone cupone)
        {
            if (id != cupone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cupone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuponeExists(cupone.Id))
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
            return View(cupone);
        }

        // GET: Cupones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cupone = await _context.Cupones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cupone == null)
            {
                return NotFound();
            }

            return View(cupone);
        }

        // POST: Cupones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cupone = await _context.Cupones.FindAsync(id);
            if (cupone != null)
            {
                _context.Cupones.Remove(cupone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuponeExists(int id)
        {
            return _context.Cupones.Any(e => e.Id == id);
        }
    }
}
