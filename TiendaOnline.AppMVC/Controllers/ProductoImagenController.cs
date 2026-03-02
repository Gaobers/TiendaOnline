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
    public class ProductoImagenController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public ProductoImagenController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: ProductoImagen
        public async Task<IActionResult> Index()
        {
            var tiendaOnlineZapContext = _context.ProductoImagens.Include(p => p.Producto);
            return View(await tiendaOnlineZapContext.ToListAsync());
        }

        // GET: ProductoImagen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoImagen = await _context.ProductoImagens
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ProductoImagenId == id);
            if (productoImagen == null)
            {
                return NotFound();
            }

            return View(productoImagen);
        }

        // GET: ProductoImagen/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre"); return View();
        }

        // POST: ProductoImagen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoImagenId,ProductoId,Url,EsPrincipal")] ProductoImagen productoImagen)
        {
            productoImagen.FechaRegistro = DateTime.Now;

            productoImagen.Url = productoImagen.Url?.Trim();

            ModelState.Remove("Producto");
            if (ModelState.IsValid)
            {
                if (productoImagen.EsPrincipal)
                {
                    var otras = await _context.ProductoImagens
                        .Where(x => x.ProductoId == productoImagen.ProductoId && x.EsPrincipal)
                        .ToListAsync();

                    foreach (var o in otras)
                        o.EsPrincipal = false;
                }

                _context.Add(productoImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", productoImagen.ProductoId);
            return View(productoImagen);
        }
        // GET: ProductoImagen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoImagen = await _context.ProductoImagens.FindAsync(id);
            if (productoImagen == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "ProductoId", productoImagen.ProductoId);
            return View(productoImagen);
        }

        // POST: ProductoImagen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoImagenId,ProductoId,Url,EsPrincipal")] ProductoImagen productoImagen)
        {
            if (id != productoImagen.ProductoImagenId)
                return NotFound();

            var original = await _context.ProductoImagens.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductoImagenId == id);

            if (original == null) return NotFound();

            productoImagen.FechaRegistro = original.FechaRegistro;
            productoImagen.Url = productoImagen.Url?.Trim();

            if (ModelState.IsValid)
            {
                if (productoImagen.EsPrincipal)
                {
                    var otras = await _context.ProductoImagens
                        .Where(x => x.ProductoId == productoImagen.ProductoId && x.EsPrincipal && x.ProductoImagenId != id)
                        .ToListAsync();

                    foreach (var o in otras)
                        o.EsPrincipal = false;
                }

                _context.Update(productoImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", productoImagen.ProductoId);
            return View(productoImagen);
        }
        // GET: ProductoImagen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoImagen = await _context.ProductoImagens
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ProductoImagenId == id);
            if (productoImagen == null)
            {
                return NotFound();
            }

            return View(productoImagen);
        }

        // POST: ProductoImagen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoImagen = await _context.ProductoImagens.FindAsync(id);
            if (productoImagen != null)
            {
                _context.ProductoImagens.Remove(productoImagen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoImagenExists(int id)
        {
            return _context.ProductoImagens.Any(e => e.ProductoImagenId == id);
        }
    }
}
