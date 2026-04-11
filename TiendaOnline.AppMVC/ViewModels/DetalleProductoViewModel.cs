namespace TiendaOnline.AppMVC.ViewModels
{
    public class ProductoDetalleViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }
        public string? Marca { get; set; }
        public string? Sku { get; set; }
        public string? Genero { get; set; }
        public string? Material { get; set; }
        public bool EsDestacado { get; set; }

        public List<string> Colores { get; set; } = new();
        public List<string> Imagenes { get; set; } = new();

        // opcional
        public List<string> TallasDisponibles { get; set; } = new();
    }
}