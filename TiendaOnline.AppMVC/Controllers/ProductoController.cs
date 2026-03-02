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
    public class ProductoController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public ProductoController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Precio,Descripcion,Estatus")] Producto producto)
        {
            NormalizarYValidarProducto(producto, esEdicion: false);

            // Validación de nombre único
            if (!string.IsNullOrWhiteSpace(producto.Nombre))
            {
                bool existe = await _context.Productos.AnyAsync(p => p.Nombre == producto.Nombre);
                if (existe)
                    ModelState.AddModelError("Nombre", "Ya existe un producto con ese nombre.");
            }

            if (!ModelState.IsValid)
                return View(producto);

            _context.Add(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // POST: Producto/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Precio,Descripcion,Estatus")] Producto producto)
        {
            if (id != producto.ProductoId)
                return NotFound();

            // Traer el registro original para proteger FechaRegistro
            var original = await _context.Productos.AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductoId == id);

            if (original == null)
                return NotFound();

            // Mantener FechaRegistro original (no editable)
            producto.FechaRegistro = original.FechaRegistro;

            NormalizarYValidarProducto(producto, esEdicion: true);

            // Validación de nombre único (excluyendo el mismo ID)
            if (!string.IsNullOrWhiteSpace(producto.Nombre))
            {
                bool existe = await _context.Productos.AnyAsync(p =>
                    p.Nombre == producto.Nombre && p.ProductoId != producto.ProductoId);

                if (existe)
                    ModelState.AddModelError("Nombre", "Ya existe un producto con ese nombre.");
            }

            if (!ModelState.IsValid)
                return View(producto);

            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(producto.ProductoId))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }

        private void NormalizarYValidarProducto(Producto producto, bool esEdicion)
        {
            // Normalizar
            producto.Nombre = producto.Nombre?.Trim();
            producto.Descripcion = producto.Descripcion?.Trim();

            // Validaciones sencillas (servidor)
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");

            if (producto.Nombre != null && producto.Nombre.Length > 80)
                ModelState.AddModelError("Nombre", "Máximo 80 caracteres.");

            if (producto.Precio <= 0)
                ModelState.AddModelError("Precio", "El precio debe ser mayor que 0.");

            if (producto.Descripcion != null && producto.Descripcion.Length > 500)
                ModelState.AddModelError("Descripcion", "Máximo 500 caracteres.");

            // Defaults / reglas de fechas
            if (!esEdicion)
            {
                // si tu DB ya tiene DEFAULT GETDATE(), esto igual no estorba
                if (producto.FechaRegistro == default)
                    producto.FechaRegistro = DateTime.Now;

                // si quieres que por defecto quede activo
                // producto.Estatus = producto.Estatus == 0 ? (byte)1 : producto.Estatus;
            }
            else
            {
                producto.FechaActualizacion = DateTime.Now;
            }
        }
    }
}
