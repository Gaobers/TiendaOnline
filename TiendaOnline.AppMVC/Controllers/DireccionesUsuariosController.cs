using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class DireccionesUsuariosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public DireccionesUsuariosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: DireccionesUsuarios
        //Filtros
        public async Task<IActionResult> Index(string alias, byte? estatus)
        {
            var query = _context.DireccionesUsuarios
                .Include(d => d.Usuario)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(alias))
                query = query.Where(d => d.Alias != null && d.Alias.Contains(alias));

            if (estatus.HasValue)
                query = query.Where(d => d.Estatus == estatus);

            var direccionesUsuarios = await query
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(direccionesUsuarios);
        }

        // GET: DireccionesUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionesUsuario = await _context.DireccionesUsuarios
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionesUsuario == null)
            {
                return NotFound();
            }

            return View(direccionesUsuario);
        }

        // GET: DireccionesUsuarios/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo");
            return View();
        }

        // POST: DireccionesUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Alias,Departamento,Municipio,DireccionExacta,Referencia,TelefonoContacto,EsPrincipal")] DireccionesUsuario direccionesUsuario)
        {
            ModelState.Remove("Usuario");
            ModelState.Remove("Pedidos");

            if (ModelState.IsValid)
            {
                direccionesUsuario.Estatus = 1;
                direccionesUsuario.FechaCreacion = DateTime.Now;
                direccionesUsuario.FechaActualizacion = null;

                _context.Add(direccionesUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo", direccionesUsuario.UsuarioId);
            return View(direccionesUsuario);
        }

        // GET: DireccionesUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionesUsuario = await _context.DireccionesUsuarios.FindAsync(id);
            if (direccionesUsuario == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo", direccionesUsuario.UsuarioId);
            return View(direccionesUsuario);
        }

        // POST: DireccionesUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Alias,Departamento,Municipio,DireccionExacta,Referencia,TelefonoContacto,EsPrincipal,Estatus")] DireccionesUsuario direccionesUsuario)
        {
            if (id != direccionesUsuario.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Usuario");
            ModelState.Remove("Pedidos");

            if (ModelState.IsValid)
            {
                try
                {
                    var direccionDb = await _context.DireccionesUsuarios.FindAsync(id);

                    if (direccionDb == null)
                    {
                        return NotFound();
                    }

                    direccionDb.UsuarioId = direccionesUsuario.UsuarioId;
                    direccionDb.Alias = direccionesUsuario.Alias;
                    direccionDb.Departamento = direccionesUsuario.Departamento;
                    direccionDb.Municipio = direccionesUsuario.Municipio;
                    direccionDb.DireccionExacta = direccionesUsuario.DireccionExacta;
                    direccionDb.Referencia = direccionesUsuario.Referencia;
                    direccionDb.TelefonoContacto = direccionesUsuario.TelefonoContacto;
                    direccionDb.EsPrincipal = direccionesUsuario.EsPrincipal;
                    direccionDb.Estatus = direccionesUsuario.Estatus;
                    direccionDb.FechaActualizacion = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionesUsuarioExists(direccionesUsuario.Id))
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

            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Correo", direccionesUsuario.UsuarioId);
            return View(direccionesUsuario);
        }

        // GET: DireccionesUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionesUsuario = await _context.DireccionesUsuarios
                .Include(d => d.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionesUsuario == null)
            {
                return NotFound();
            }

            return View(direccionesUsuario);
        }

        // POST: DireccionesUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccionesUsuario = await _context.DireccionesUsuarios.FindAsync(id);
            if (direccionesUsuario != null)
            {
                _context.DireccionesUsuarios.Remove(direccionesUsuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionesUsuarioExists(int id)
        {
            return _context.DireccionesUsuarios.Any(e => e.Id == id);
        }
    }
}
