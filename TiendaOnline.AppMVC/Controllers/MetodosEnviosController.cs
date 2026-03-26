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
    public class MetodosEnviosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public MetodosEnviosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: MetodosEnvios
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetodosEnvios.ToListAsync());
        }

        // GET: MetodosEnvios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosEnvio = await _context.MetodosEnvios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodosEnvio == null)
            {
                return NotFound();
            }

            return View(metodosEnvio);
        }

        // GET: MetodosEnvios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodosEnvios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Costo,TiempoEstimado,Estatus,FechaCreacion")] MetodosEnvio metodosEnvio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodosEnvio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodosEnvio);
        }

        // GET: MetodosEnvios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosEnvio = await _context.MetodosEnvios.FindAsync(id);
            if (metodosEnvio == null)
            {
                return NotFound();
            }
            return View(metodosEnvio);
        }

        // POST: MetodosEnvios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Costo,TiempoEstimado,Estatus,FechaCreacion")] MetodosEnvio metodosEnvio)
        {
            if (id != metodosEnvio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodosEnvio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodosEnvioExists(metodosEnvio.Id))
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
            return View(metodosEnvio);
        }

        // GET: MetodosEnvios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodosEnvio = await _context.MetodosEnvios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodosEnvio == null)
            {
                return NotFound();
            }

            return View(metodosEnvio);
        }

        // POST: MetodosEnvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodosEnvio = await _context.MetodosEnvios.FindAsync(id);
            if (metodosEnvio != null)
            {
                _context.MetodosEnvios.Remove(metodosEnvio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodosEnvioExists(int id)
        {
            return _context.MetodosEnvios.Any(e => e.Id == id);
        }
    }
}
