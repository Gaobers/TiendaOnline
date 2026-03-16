using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;
using TiendaOnline.AppMVC.Models.ViewModels;

public class HomeController : Controller
{
    private readonly TiendaOnlineZapContext _context;

    public HomeController(TiendaOnlineZapContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var vm = new DashboardVM
        {
            TotalProductos = await _context.Productos.CountAsync(),
            TotalPedidos = await _context.Pedidos.CountAsync(),
            TotalUsuarios = await _context.Usuarios.CountAsync(),

            ProductosRecientes = await _context.Productos
                .OrderByDescending(p => p.FechaCreacion)
                .Take(5)
                .ToListAsync()
        };

        return View(vm);
    }
}