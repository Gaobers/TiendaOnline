using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.AppMVC.Models;

namespace TiendaOnline.AppMVC.Models;

public partial class TiendaOnlineZapContext : DbContext
{
    public TiendaOnlineZapContext()
    {
    }

    public TiendaOnlineZapContext(DbContextOptions<TiendaOnlineZapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AjusteInventario> AjusteInventarios { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoImagen> ProductoImagens { get; set; }

    public virtual DbSet<Talla> Tallas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AjusteInventario>(entity =>
        {
            entity.HasKey(e => e.AjusteInventarioId).HasName("PK__AjusteIn__5D9286532D959885");

            entity.ToTable("AjusteInventario");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Inventario).WithMany(p => p.AjusteInventarios)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AjusteInventario_Inventario");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.InventarioId).HasName("PK__Inventar__FB8A24D7C9EC2427");

            entity.ToTable("Inventario");

            entity.HasIndex(e => new { e.ProductoId, e.TallaId }, "UQ_Inventario_Producto_Talla").IsUnique();

            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Producto");

            entity.HasOne(d => d.Talla).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.TallaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Talla");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PK__Pedido__09BA1430D944A24F");

            entity.ToTable("Pedido");

            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EmailCliente)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDIENTE");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.HasKey(e => e.PedidoDetalleId).HasName("PK__PedidoDe__12156C4377E668FC");

            entity.ToTable("PedidoDetalle");

            entity.HasIndex(e => new { e.PedidoId, e.ProductoId, e.TallaId }, "UQ_PedidoDetalle").IsUnique();

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Pedido).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidoDetalle_Pedido");

            // entity.HasOne(d => d.Talla).WithMany(p => p.PedidoDetalles)
    //.HasForeignKey(d => d.TallaId)
    //.OnDelete(DeleteBehavior.ClientSetNull)
    //.HasConstraintName("FK_PedidoDetalle_Talla");

            entity.HasOne(d => d.Talla).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.TallaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidoDetalle_Talla");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AEA3C8F52465");

            entity.ToTable("Producto");

            entity.HasIndex(e => e.Nombre, "UQ__Producto__75E3EFCFF3D3F582").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<ProductoImagen>(entity =>
        {
            entity.HasKey(e => e.ProductoImagenId).HasName("PK__Producto__2DA9E6096F42F20A");

            entity.ToTable("ProductoImagen");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoImagens)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductoImagen_Producto");
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.TallaId).HasName("PK__Talla__9BF1376D701FB4E1");

            entity.ToTable("Talla");

            entity.HasIndex(e => e.Numero, "UQ__Talla__7E532BC6752DE949").IsUnique();

            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE7B8550B75B7");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A19B064666A").IsUnique();

            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("CLIENTE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
