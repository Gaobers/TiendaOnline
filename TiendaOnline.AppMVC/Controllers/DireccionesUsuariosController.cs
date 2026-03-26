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
    public class DireccionesUsuariosController : Controller
    {
        private readonly TiendaOnlineZapContext _context;

        public DireccionesUsuariosController(TiendaOnlineZapContext context)
        {
            _context = context;
        }

        // GET: DireccionesUsuarios
        public async Task<IActionResult> Index()
        {
            var tiendaOnlineZapContext = _context.DireccionesUsuarios.Include(d => d.Usuario);
            return View(await tiendaOnlineZapContext.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Alias,Departamento,Municipio,DireccionExacta,Referencia,TelefonoContacto,EsPrincipal,Estatus,FechaCreacion,FechaActualizacion")] DireccionesUsuario direccionesUsuario)
        {
            if (ModelState.IsValid)
            {
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Alias,Departamento,Municipio,DireccionExacta,Referencia,TelefonoContacto,EsPrincipal,Estatus,FechaCreacion,FechaActualizacion")] DireccionesUsuario direccionesUsuario)
        {
            if (id != direccionesUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccionesUsuario);
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
