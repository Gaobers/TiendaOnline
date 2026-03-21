using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaOnline.AppMVC.Models;

public partial class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorios")]
    [StringLength(45, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 45 caracteres.")]
    [Display(Name = "Nombre del Producto")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 10000.00, ErrorMessage = "El precio debe estar entre 0.01 y 10,000")]
    [DataType(DataType.Currency)]
    public decimal Precio { get; set; }

    [StringLength(500, MinimumLength = 3, ErrorMessage = "La descripción debe tener entre 3 y 500 caracteres")]
    [Display(Name = "Descripción del Producto")]
    public string? Descripcion { get; set; }


    public byte Estatus { get; set; }

    public DateTime FechaCreacion { get; set; }


    [Display(Name = "Fecha de Actualización")]
    public DateTime? FechaActualizacion { get; set; }


    [Required(ErrorMessage = "La categoría es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una categoría válida.")]
    [Display(Name = "Categoría")]
    public int CategoriaId { get; set; }


    [Required(ErrorMessage = "La marca es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar una marca válida.")]
    [Display(Name = "Marca")]
    public int MarcaId { get; set; }


    [Required(ErrorMessage = "El SKU es obligatorio.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "El SKU debe tener entre 3 y 30 caracteres.")]
    [Display(Name = "SKU")]
    public string Sku { get; set; } = null!;


    [StringLength(10, ErrorMessage = "El campo no puede exceder 10 caracteres")]
    public string? Genero { get; set; }


    [StringLength(20, ErrorMessage = "El material no puede exceder 20 caracteres.")]
    public string? Material { get; set; }


    [Display(Name = "Destacado")]
    public bool EsDestacado { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual Marca Marca { get; set; } = null!;

    public virtual ICollection<ProductosColore> ProductosColores { get; set; } = new List<ProductosColore>();

    public virtual ICollection<ProductosImagene> ProductosImagenes { get; set; } = new List<ProductosImagene>();
}
