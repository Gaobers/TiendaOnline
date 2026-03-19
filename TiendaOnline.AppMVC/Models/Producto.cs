using System;
using System.Collections.Generic;

namespace TiendaOnline.AppMVC.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public string? Descripcion { get; set; }

    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public int CategoriaId { get; set; }

    public int MarcaId { get; set; }

    public string Sku { get; set; } = null!;

    public string? Genero { get; set; }

    public string? Material { get; set; }

    public bool EsDestacado { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual Marca Marca { get; set; } = null!;

    public virtual ICollection<ProductosColore> ProductosColores { get; set; } = new List<ProductosColore>();

    public virtual ICollection<ProductosImagene> ProductosImagenes { get; set; } = new List<ProductosImagene>();
}
