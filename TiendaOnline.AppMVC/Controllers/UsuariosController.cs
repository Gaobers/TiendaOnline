using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public UsuariosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // =========================
        // LOGIN
        // =========================

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string correo, string password, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.Correo = correo;

            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("", "Correo y contraseña son obligatorios.");
                return View();
            }

            var usuarioDB = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Estatus == 1);

            if (usuarioDB == null)
            {
                ModelState.AddModelError("", "Credenciales incorrectas.");
                return View();
            }

            try
            {
                bool pareceHashBCrypt = !string.IsNullOrWhiteSpace(usuarioDB.PasswordHash) &&
                                        (usuarioDB.PasswordHash.StartsWith("$2a$") ||
                                         usuarioDB.PasswordHash.StartsWith("$2b$") ||
                                         usuarioDB.PasswordHash.StartsWith("$2y$"));

                if (!pareceHashBCrypt)
                {
                    ModelState.AddModelError("", "Este usuario tiene una contraseña antigua o inválida. Debe actualizarse.");
                    return View();
                }

                bool esValido = BCrypt.Net.BCrypt.Verify(password, usuarioDB.PasswordHash);

                if (!esValido)
                {
                    ModelState.AddModelError("", "Credenciales incorrectas.");
                    return View();
                }
            }
            catch
            {
                ModelState.AddModelError("", "No fue posible validar la contraseña del usuario.");
                return View();
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDB.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuarioDB.Nombre),
                    new Claim(ClaimTypes.Email, usuarioDB.Correo),
                    new Claim(ClaimTypes.Role, usuarioDB.Rol.Nombre),
                    new Claim("Id", usuarioDB.Id.ToString()),
                    new Claim("Correo", usuarioDB.Correo),
                    new Claim("RolId", usuarioDB.RolId.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // =========================
        // CRUD USUARIOS
        // =========================

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(string nombre, byte? estatus)
        {
            var query = _context.Usuarios
                .Include(u => u.Rol)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nombre))
                query = query.Where(u => u.Nombre.Contains(nombre));

            if (estatus.HasValue)
                query = query.Where(u => u.Estatus == estatus.Value);


            var usuarios = await query
                .OrderByDescending(c => c.Id)
                .ToListAsync();

            return View(usuarios);
        }

        [Authorize(Roles = "Administrador")]
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

        //[Authorize(Roles = "Administrador")]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            await CargarRolesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        //[Authorize(Roles = "Administrador")]
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

            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.PasswordHash);
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaActualizacion = null;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador")]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Correo,Telefono,Estatus,RolId")] Usuario usuario)
        {
            if (id != usuario.Id)
                return NotFound();

            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            if (usuarioDb == null)
                return NotFound();

            ModelState.Remove("Rol");
            ModelState.Remove("AjustesInventarios");
            ModelState.Remove("Carritos");
            ModelState.Remove("DireccionesUsuarios");
            ModelState.Remove("HistorialesEstadosPedidos");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Pedidos");
            ModelState.Remove("UsosCupones");
            ModelState.Remove("PasswordHash");
            ModelState.Remove("FechaCreacion");
            ModelState.Remove("FechaActualizacion");

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
                usuario.PasswordHash = usuarioDb.PasswordHash;
                usuario.FechaCreacion = usuarioDb.FechaCreacion;
                usuario.FechaActualizacion = usuarioDb.FechaActualizacion;

                await CargarRolesAsync(usuario.RolId);
                return View(usuario);
            }

            usuarioDb.Nombre = usuario.Nombre;
            usuarioDb.Apellido = usuario.Apellido;
            usuarioDb.Correo = usuario.Correo;
            usuarioDb.Telefono = usuario.Telefono;
            usuarioDb.Estatus = usuario.Estatus;
            usuarioDb.RolId = usuario.RolId;
            usuarioDb.FechaActualizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrador")]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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

        // =========================
        // CAMBIO DE CONTRASEÑA
        // =========================

        public async Task<IActionResult> ChangePassword()
        {
            var userIdClaim = User.FindFirst("Id");

            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var usuario = await _context.Usuarios.FindAsync(userId);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, string passwordNueva, string confirmarPassword)
        {
            if (string.IsNullOrWhiteSpace(passwordNueva))
                ModelState.AddModelError("passwordNueva", "La nueva contraseña es obligatoria.");

            if (string.IsNullOrWhiteSpace(confirmarPassword))
                ModelState.AddModelError("confirmarPassword", "Debe confirmar la contraseña.");

            if (passwordNueva != confirmarPassword)
                ModelState.AddModelError("confirmarPassword", "Las contraseñas no coinciden.");

            if (!ModelState.IsValid)
            {
                var usuarioError = await _context.Usuarios.FindAsync(id);
                return View(usuarioError);
            }

            var usuarioData = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            if (usuarioData == null)
                return NotFound();

            usuarioData.PasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordNueva);
            usuarioData.FechaActualizacion = DateTime.Now;

            _context.Update(usuarioData);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Contraseña actualizada correctamente.";
            return RedirectToAction(nameof(Login));
        }

        // =========================
        // MÉTODOS PRIVADOS
        // =========================

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        private async Task CargarRolesAsync(int? rolId = null)
        {
            var roles = await _context.Roles
                .Where(r => r.Estatus == 1)
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

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");

            if (!string.IsNullOrWhiteSpace(usuario.Nombre) && usuario.Nombre.Length > 100)
                ModelState.AddModelError("Nombre", "Máximo 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(usuario.Apellido) && usuario.Apellido.Length > 100)
                ModelState.AddModelError("Apellido", "Máximo 100 caracteres.");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                ModelState.AddModelError("Correo", "El correo es obligatorio.");

            if (!string.IsNullOrWhiteSpace(usuario.Correo) && usuario.Correo.Length > 150)
                ModelState.AddModelError("Correo", "Máximo 150 caracteres.");

            if (!string.IsNullOrWhiteSpace(usuario.Telefono) && usuario.Telefono.Length > 20)
                ModelState.AddModelError("Telefono", "Máximo 20 caracteres.");

            if (!esEdicion)
            {
                if (string.IsNullOrWhiteSpace(usuario.PasswordHash))
                    ModelState.AddModelError("PasswordHash", "La contraseña es obligatoria.");

                if (!string.IsNullOrWhiteSpace(usuario.PasswordHash) && usuario.PasswordHash.Length > 300)
                    ModelState.AddModelError("PasswordHash", "Máximo 300 caracteres.");
            }

            if (usuario.RolId <= 0)
                ModelState.AddModelError("RolId", "Debe seleccionar un rol.");

            if (!new byte[] { 1, 2, 3, 4 }.Contains(usuario.Estatus))
                ModelState.AddModelError("Estatus", "Debe seleccionar un estado válido.");

            if (!esEdicion && usuario.FechaCreacion == default)
                usuario.FechaCreacion = DateTime.Now;
        }
    }
}