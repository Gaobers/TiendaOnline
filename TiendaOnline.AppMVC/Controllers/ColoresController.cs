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
    public class ColoresController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public ColoresController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Colores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Colores.ToListAsync());
        }

        // GET: Colores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colore = await _context.Colores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colore == null)
            {
                return NotFound();
            }

            return View(colore);
        }

        // GET: Colores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,CodigoHex,Estatus,FechaCreacion")] Colore colore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colore);
        }

        // GET: Colores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colore = await _context.Colores.FindAsync(id);
            if (colore == null)
            {
                return NotFound();
            }
            return View(colore);
        }

        // POST: Colores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,CodigoHex,Estatus,FechaCreacion")] Colore colore)
        {
            if (id != colore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColoreExists(colore.Id))
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
            return View(colore);
        }

        // GET: Colores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colore = await _context.Colores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colore == null)
            {
                return NotFound();
            }

            return View(colore);
        }

        // POST: Colores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var colore = await _context.Colores.FindAsync(id);
            if (colore != null)
            {
                _context.Colores.Remove(colore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColoreExists(int id)
        {
            return _context.Colores.Any(e => e.Id == id);
        }
    }
}
