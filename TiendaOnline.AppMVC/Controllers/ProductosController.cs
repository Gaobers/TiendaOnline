using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;
using TiendaOnline.AppMVC.ViewModels;

namespace TiendaOnline.AppMVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public ProductosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(string nombre, byte? estatus, int top = 10)
        {
            var query = _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.ProductosColores)
                    .ThenInclude(pc => pc.Color)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(p => p.Nombre.Contains(nombre));

            if (estatus.HasValue)
                query = query.Where(p => p.Estatus == estatus.Value);

            query = query.Take(top);

            var productos = await query.ToListAsync();
            return View(productos);
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.ProductosColores)
                    .ThenInclude(pc => pc.Color)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // GET: Productos/Create
        public async Task<IActionResult> Create()
        {
            var model = new ProductoFormViewModel
            {
                ColoresDisponibles = await _context.Colores
                    .Where(c => c.Estatus == 1)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre
                    })
                    .ToListAsync()
            };

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre");

            return View(model);
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var producto = new Producto
                {
                    Nombre = model.Nombre,
                    Precio = model.Precio,
                    Descripcion = model.Descripcion,
                    CategoriaId = model.CategoriaId,
                    MarcaId = model.MarcaId,
                    Sku = model.Sku,
                    Genero = model.Genero,
                    Material = model.Material,
                    EsDestacado = model.EsDestacado,
                    Estatus = 1,
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now
                };

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                if (model.ColoresSeleccionados != null && model.ColoresSeleccionados.Any())
                {
                    foreach (var colorId in model.ColoresSeleccionados.Distinct())
                    {
                        _context.ProductosColores.Add(new ProductosColore
                        {
                            ProductoId = producto.Id,
                            ColorId = colorId
                        });
                    }

                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            model.ColoresDisponibles = await _context.Colores
                .Where(c => c.Estatus == 1)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
                .ToListAsync();

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", model.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", model.MarcaId);

            return View(model);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.ProductosColores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            var model = new ProductoFormViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
                Estatus = producto.Estatus,
                FechaCreacion = producto.FechaCreacion,
                FechaActualizacion = producto.FechaActualizacion,
                CategoriaId = producto.CategoriaId,
                MarcaId = producto.MarcaId,
                Sku = producto.Sku,
                Genero = producto.Genero,
                Material = producto.Material,
                EsDestacado = producto.EsDestacado,
                ColoresSeleccionados = producto.ProductosColores.Select(pc => pc.ColorId).ToList(),
                ColoresDisponibles = await _context.Colores
                    .Where(c => c.Estatus == 1)
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nombre
                    })
                    .ToListAsync()
            };

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", model.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", model.MarcaId);

            return View(model);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var producto = await _context.Productos
                        .Include(p => p.ProductosColores)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    if (producto == null) return NotFound();

                    producto.Nombre = model.Nombre;
                    producto.Precio = model.Precio;
                    producto.Descripcion = model.Descripcion;
                    producto.Estatus = model.Estatus;
                    producto.CategoriaId = model.CategoriaId;
                    producto.MarcaId = model.MarcaId;
                    producto.Sku = model.Sku;
                    producto.Genero = model.Genero;
                    producto.Material = model.Material;
                    producto.EsDestacado = model.EsDestacado;
                    producto.FechaActualizacion = DateTime.Now;

                    if (producto.ProductosColores.Any())
                    {
                        _context.ProductosColores.RemoveRange(producto.ProductosColores);
                    }

                    if (model.ColoresSeleccionados != null && model.ColoresSeleccionados.Any())
                    {
                        foreach (var colorId in model.ColoresSeleccionados.Distinct())
                        {
                            _context.ProductosColores.Add(new ProductosColore
                            {
                                ProductoId = producto.Id,
                                ColorId = colorId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(model.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            model.ColoresDisponibles = await _context.Colores
                .Where(c => c.Estatus == 1)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nombre
                })
                .ToListAsync();

            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Nombre", model.CategoriaId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "Id", "Nombre", model.MarcaId);

            return View(model);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.ProductosColores)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto != null)
            {
                if (producto.ProductosColores.Any())
                    _context.ProductosColores.RemoveRange(producto.ProductosColores);

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DetallePublico(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Include(p => p.ProductosColores)
                    .ThenInclude(pc => pc.Color)
                .Include(p => p.ProductosImagenes)
                .Include(p => p.Inventarios)
                    .ThenInclude(i => i.Talla)
                .FirstOrDefaultAsync(p => p.Id == id && p.Estatus == 1);

            if (producto == null)
                return NotFound();

            var vm = new ProductoDetalleViewModel
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
                Categoria = producto.Categoria?.Nombre,
                Marca = producto.Marca?.Nombre,
                Sku = producto.Sku,
                Genero = producto.Genero,
                Material = producto.Material,
                EsDestacado = producto.EsDestacado,

                Colores = producto.ProductosColores
                    .Where(pc => pc.Color != null)
                    .Select(pc => pc.Color.Nombre)
                    .Distinct()
                    .ToList(),

                TallasDisponibles = producto.Inventarios
                    .Where(i => i.Stock > 0)
                    .Select(i => i.Talla.Numero.ToString())
                    .Distinct()
                    .ToList()
            };

            return View(vm);
        }
    }
}