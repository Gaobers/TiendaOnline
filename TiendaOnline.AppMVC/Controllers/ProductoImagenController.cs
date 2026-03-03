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
            if (id == null) return NotFound();

            var productoImagen = await _context.ProductoImagens
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ProductoImagenId == id);

            if (productoImagen == null) return NotFound();

            return View(productoImagen);
        }

        // GET: ProductoImagen/Create
        public IActionResult Create()
        {
            // Cambié "Nombre" por "Nombre" (asegúrate que tu modelo Producto tenga esa propiedad)
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoImagenId,ProductoId,Url,EsPrincipal")] ProductoImagen productoImagen)
        {
            // CORRECCIÓN: Si Url viene nulo, asignamos un string vacío para evitar errores de referencia
            productoImagen.Url = productoImagen.Url?.Trim() ?? "";
            productoImagen.FechaRegistro = DateTime.Now;

            // Eliminamos "Producto" del ModelState porque es una propiedad de navegación y siempre será null en el POST
            ModelState.Remove("Producto");

            if (ModelState.IsValid)
            {
                if (productoImagen.EsPrincipal)
                {
                    var otras = await _context.ProductoImagens
                        .Where(x => x.ProductoId == productoImagen.ProductoId && x.EsPrincipal)
                        .ToListAsync();

                    foreach (var o in otras) o.EsPrincipal = false;
                }

                _context.Add(productoImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", productoImagen.ProductoId);
            return View(productoImagen);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var productoImagen = await _context.ProductoImagens.FindAsync(id);
            if (productoImagen == null) return NotFound();

            // CORRECCIÓN: Usar "Nombre" en lugar de "ProductoId" para que el dropdown sea legible
            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", productoImagen.ProductoId);
            return View(productoImagen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoImagenId,ProductoId,Url,EsPrincipal")] ProductoImagen productoImagen)
        {
            if (id != productoImagen.ProductoImagenId) return NotFound();

            // Usamos AsNoTracking para comparar sin bloquear la base de datos
            var original = await _context.ProductoImagens.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductoImagenId == id);

            if (original == null) return NotFound();

            // CORRECCIÓN: Asegurar que no sea nulo antes de guardar
            productoImagen.Url = productoImagen.Url?.Trim() ?? original.Url;
            productoImagen.FechaRegistro = original.FechaRegistro;

            ModelState.Remove("Producto");

            if (ModelState.IsValid)
            {
                if (productoImagen.EsPrincipal)
                {
                    var otras = await _context.ProductoImagens
                        .Where(x => x.ProductoId == productoImagen.ProductoId && x.EsPrincipal && x.ProductoImagenId != id)
                        .ToListAsync();

                    foreach (var o in otras) o.EsPrincipal = false;
                }

                _context.Update(productoImagen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductoId"] = new SelectList(_context.Productos, "ProductoId", "Nombre", productoImagen.ProductoId);
            return View(productoImagen);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var productoImagen = await _context.ProductoImagens
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ProductoImagenId == id);

            if (productoImagen == null) return NotFound();

            return View(productoImagen);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoImagen = await _context.ProductoImagens.FindAsync(id);
            if (productoImagen != null)
            {
                _context.ProductoImagens.Remove(productoImagen);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoImagenExists(int id)
        {
            return _context.ProductoImagens.Any(e => e.ProductoImagenId == id);
        }
    }
}