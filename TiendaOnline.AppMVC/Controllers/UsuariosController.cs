using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public UsuariosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        //Fitlros
        public async Task<IActionResult> Index(string nombre, byte? estatus, int top = 10)
        {
            var query = _context.Usuarios
                .Include(u => u.Rol)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(u => u.Nombre.Contains(nombre));

            if (estatus.HasValue)
                query = query.Where(u => u.Estatus == estatus.Value);

            query = query.Take(top);

            var usuarios = await query.ToListAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // GET: Usuarios/Create
        public async Task<IActionResult> Create()
        {
            await CargarRolesAsync();
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Apellido,Correo,Telefono,PasswordHash,Estatus,RolId")] Usuario usuario)
        {
            ModelState.Remove("Rol");
            ModelState.Remove("AjustesInventarios");
            ModelState.Remove("Carritos");
            ModelState.Remove("DireccionesUsuarios");
            ModelState.Remove("HistorialesEstadosPedidos");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Pedidos");
            ModelState.Remove("UsosCupones");

            NormalizarYValidarUsuario(usuario, esEdicion: false);

            if (!string.IsNullOrWhiteSpace(usuario.Correo))
            {
                bool existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo);
                if (existeCorreo)
                    ModelState.AddModelError("Correo", "Ya existe un usuario con ese correo.");
            }

            bool rolValido = await _context.Roles.AnyAsync(r => r.Id == usuario.RolId);
            if (!rolValido)
                ModelState.AddModelError("RolId", "Debe seleccionar un rol válido.");

            if (!ModelState.IsValid)
            {
                await CargarRolesAsync(usuario.RolId);
                return View(usuario);
            }

            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaActualizacion = null;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            await CargarRolesAsync(usuario.RolId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Correo,Telefono,PasswordHash,Estatus,FechaCreacion,FechaActualizacion,RolId")] Usuario usuario)
        {
            if (id != usuario.Id)
                return NotFound();

            var original = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (original == null)
                return NotFound();

            ModelState.Remove("Rol");
            ModelState.Remove("AjustesInventarios");
            ModelState.Remove("Carritos");
            ModelState.Remove("DireccionesUsuarios");
            ModelState.Remove("HistorialesEstadosPedidos");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Pedidos");
            ModelState.Remove("UsosCupones");

            NormalizarYValidarUsuario(usuario, esEdicion: true);

            if (!string.IsNullOrWhiteSpace(usuario.Correo))
            {
                bool existeCorreo = await _context.Usuarios.AnyAsync(u =>
                    u.Correo == usuario.Correo && u.Id != usuario.Id);

                if (existeCorreo)
                    ModelState.AddModelError("Correo", "Ya existe un usuario con ese correo.");
            }

            bool rolValido = await _context.Roles.AnyAsync(r => r.Id == usuario.RolId);
            if (!rolValido)
                ModelState.AddModelError("RolId", "Debe seleccionar un rol válido.");

            if (!ModelState.IsValid)
            {
                await CargarRolesAsync(usuario.RolId);
                return View(usuario);
            }

            usuario.FechaCreacion = original.FechaCreacion;
            usuario.FechaActualizacion = DateTime.Now;

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Pedidos)
                .Include(u => u.Carritos)
                .Include(u => u.DireccionesUsuarios)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
                return RedirectToAction(nameof(Index));

            if (usuario.Pedidos.Any() || usuario.Carritos.Any() || usuario.DireccionesUsuarios.Any())
            {
                TempData["Error"] = "No se puede eliminar el usuario porque tiene registros relacionados.";
                return RedirectToAction(nameof(Index));
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        private async Task CargarRolesAsync(int? rolId = null)
        {
            var roles = await _context.Roles
                .OrderBy(r => r.Nombre)
                .ToListAsync();

            ViewData["RolId"] = new SelectList(roles, "Id", "Nombre", rolId);
        }

        private void NormalizarYValidarUsuario(Usuario usuario, bool esEdicion)
        {
            usuario.Nombre = usuario.Nombre?.Trim() ?? string.Empty;
            usuario.Apellido = usuario.Apellido?.Trim();
            usuario.Correo = usuario.Correo?.Trim() ?? string.Empty;
            usuario.Telefono = usuario.Telefono?.Trim();
            usuario.PasswordHash = usuario.PasswordHash?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");

            if (usuario.Nombre.Length > 100)
                ModelState.AddModelError("Nombre", "Máximo 100 caracteres.");

            if (usuario.Apellido != null && usuario.Apellido.Length > 100)
                ModelState.AddModelError("Apellido", "Máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                ModelState.AddModelError("Correo", "El correo es obligatorio.");

            if (usuario.Correo.Length > 150)
                ModelState.AddModelError("Correo", "Máximo 150 caracteres.");

            if (usuario.Telefono != null && usuario.Telefono.Length > 20)
                ModelState.AddModelError("Telefono", "Máximo 20 caracteres.");

            if (string.IsNullOrWhiteSpace(usuario.PasswordHash))
                ModelState.AddModelError("PasswordHash", "La contraseña es obligatoria.");

            if (usuario.PasswordHash.Length > 300)
                ModelState.AddModelError("PasswordHash", "Máximo 300 caracteres.");

            if (usuario.RolId <= 0)
                ModelState.AddModelError("RolId", "Debe seleccionar un rol.");

            if (!new byte[] { 1, 2, 3, 4 }.Contains(usuario.Estatus))
                ModelState.AddModelError("Estatus", "Debe seleccionar un estado válido.");

            if (!esEdicion && usuario.FechaCreacion == default)
                usuario.FechaCreacion = DateTime.Now;
        }
    }
}