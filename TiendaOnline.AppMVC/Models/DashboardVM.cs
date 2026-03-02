namespace TiendaOnline.AppMVC.Models.ViewModels
{
    public class DashboardVM
    {
        public int TotalProductos { get; set; }
        public int TotalImagenes { get; set; }
        public int TotalPedidos { get; set; }
        public int TotalUsuarios { get; set; }

        public List<Producto> ProductosRecientes { get; set; } = new();
    }
}