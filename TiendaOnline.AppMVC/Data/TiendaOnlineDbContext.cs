using Microsoft.EntityFrameworkCore;

namespace TiendaOnline.AppMVC.Data
{
    public class TiendaOnlineDbContext : DbContext
    {
        public TiendaOnlineDbContext(DbContextOptions<TiendaOnlineDbContext> options)
            : base(options)
        {
        }

        // Ejemplo:
        // public DbSet<Producto> Productos { get; set; }
    }
}