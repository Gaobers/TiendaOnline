using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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

    public virtual DbSet<AjustesInventario> AjustesInventarios { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Colore> Colores { get; set; }

    public virtual DbSet<Cupone> Cupones { get; set; }

    public virtual DbSet<DetallesCarrito> DetallesCarritos { get; set; }

    public virtual DbSet<DetallesPedido> DetallesPedidos { get; set; }

    public virtual DbSet<DireccionesUsuario> DireccionesUsuarios { get; set; }

    public virtual DbSet<EstadosPedido> EstadosPedidos { get; set; }

    public virtual DbSet<HistorialesEstadosPedido> HistorialesEstadosPedidos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MetodosEnvio> MetodosEnvios { get; set; }

    public virtual DbSet<MetodosPago> MetodosPagos { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosColore> ProductosColores { get; set; }

    public virtual DbSet<ProductosImagene> ProductosImagenes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Talla> Tallas { get; set; }

    public virtual DbSet<UsosCupone> UsosCupones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AjustesInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AjustesI__3214EC0759E41A65");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Inventario).WithMany(p => p.AjustesInventarios)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AjustesInventarios_Inventarios");

            entity.HasOne(d => d.Usuario).WithMany(p => p.AjustesInventarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AjustesInventarios_Usuarios");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Carritos__3214EC07FFE668A5");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ACTIVO");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carritos_Usuarios");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0798379AD1");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Categorias_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF351C20E8").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Colore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Colores__3214EC07C78C3D1E");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Colores_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__Colores__75E3EFCF86E3D4E6").IsUnique();

            entity.Property(e => e.CodigoHex)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cupone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cupones__3214EC07D85B54F7");

            entity.ToTable(tb =>
            {
                tb.HasCheckConstraint("CHK_Cupones_Estatus", "[Estatus] IN (0,1)");
                tb.HasCheckConstraint("CHK_Cupones_TipoDescuento", "[TipoDescuento] IN ('PORCENTAJE', 'MONTO')");
                tb.HasCheckConstraint("CHK_Cupones_Fechas", "[FechaFin] >= [FechaInicio]");
            });

            entity.HasIndex(e => e.Codigo, "UQ__Cupones__06370DAC3FE68CC8").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.MontoMinimoCompra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoDescuento)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ValorDescuento).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<DetallesCarrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3214EC07AA22E78E");

            entity.HasIndex(e => new { e.CarritoId, e.InventarioId }, "UQ_DetallesCarritos_CarritoId_InventarioId").IsUnique();

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Carrito).WithMany(p => p.DetallesCarritos)
                .HasForeignKey(d => d.CarritoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesCarritos_Carritos");

            entity.HasOne(d => d.Inventario).WithMany(p => p.DetallesCarritos)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesCarritos_Inventarios");
        });

        modelBuilder.Entity<DetallesPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalles__3214EC079ADE60F7");

            entity.Property(e => e.NombreProducto)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TallaTexto)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Inventario).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.InventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesPedidos_Inventarios");

            entity.HasOne(d => d.Pedido).WithMany(p => p.DetallesPedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallesPedidos_Pedidos");
        });

        modelBuilder.Entity<DireccionesUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Direccio__3214EC074A853243");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_DireccionesUsuarios_Estatus", "[Estatus] IN (0,1)"));

            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Departamento)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.DireccionExacta)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Municipio)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Usuario).WithMany(p => p.DireccionesUsuarios)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DireccionesUsuarios_Usuarios");
        });

        modelBuilder.Entity<EstadosPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadosP__3214EC078BEC3F4E");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_EstadosPedidos_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__EstadosP__75E3EFCFE6716995").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialesEstadosPedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC07C105584E");

            entity.Property(e => e.Comentario)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.EstadoPedido).WithMany(p => p.HistorialesEstadosPedidos)
                .HasForeignKey(d => d.EstadoPedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialesEstadosPedidos_EstadosPedidos");

            entity.HasOne(d => d.Pedido).WithMany(p => p.HistorialesEstadosPedidos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistorialesEstadosPedidos_Pedidos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.HistorialesEstadosPedidos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_HistorialesEstadosPedidos_Usuarios");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventar__3214EC07F2B9711F");

            entity.HasIndex(e => new { e.ProductoId, e.TallaId }, "UQ_Inventarios_ProductoId_TallaId").IsUnique();

            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Producto).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventarios_Productos");

            entity.HasOne(d => d.Talla).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.TallaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventarios_Tallas");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marcas__3214EC07DB268714");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Marcas_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__Marcas__75E3EFCFD2041FB1").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodosEnvio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MetodosE__3214EC070FEED193");

            entity.ToTable("MetodosEnvio", tb => tb.HasCheckConstraint("CHK_MetodosEnvio_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__MetodosE__75E3EFCF411DBC99").IsUnique();

            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TiempoEstimado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MetodosPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MetodosP__3214EC079FCA3954");

            entity.ToTable("MetodosPago", tb => tb.HasCheckConstraint("CHK_MetodosPago_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__MetodosP__75E3EFCFDE4488B0").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC075D4FBDF8");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(120)
                .IsUnicode(false);

            entity.HasOne(d => d.Pedido).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.PedidoId)
                .HasConstraintName("FK_Notificaciones_Pedidos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notificaciones_Usuarios");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pagos__3214EC07FF352D86");

            entity.Property(e => e.EstadoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReferenciaTransaccion)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.RespuestaPasarela)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.MetodoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagos_MetodosPago");

            entity.HasOne(d => d.Pedido).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pagos_Pedidos");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedido__09BA1430D944A24F");

            entity.Property(e => e.ApellidoCliente)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.CostoEnvio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DescuentoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DireccionEntrega)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EmailCliente)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ReferenciaEntrega)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TelefonoCliente)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cupon).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CuponId)
                .HasConstraintName("FK_Pedidos_Cupones");

            entity.HasOne(d => d.DireccionUsuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.DireccionUsuarioId)
                .HasConstraintName("FK_Pedidos_DireccionesUsuarios");

            entity.HasOne(d => d.EstadoPedido).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.EstadoPedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_EstadosPedidos");

            entity.HasOne(d => d.MetodoEnvio).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.MetodoEnvioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_MetodosEnvio");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedidos_Usuarios");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__A430AEA3C8F52465");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Productos_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Sku, "UQ_Productos_Sku").IsUnique();

            entity.HasIndex(e => e.Nombre, "UQ__Producto__75E3EFCFF3D3F582").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Material)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Sku)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Categorias");

            entity.HasOne(d => d.Marca).WithMany(p => p.Productos)
                .HasForeignKey(d => d.MarcaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Marcas");
        });

        modelBuilder.Entity<ProductosColore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC075A7A8784");

            entity.HasIndex(e => new { e.ProductoId, e.ColorId }, "UQ_ProductosColores_ProductoId_ColorId").IsUnique();

            entity.HasOne(d => d.Color).WithMany(p => p.ProductosColores)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductosColores_Colores");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductosColores)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductosColores_Productos");
        });

        modelBuilder.Entity<ProductosImagene>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__2DA9E6096F42F20A");

            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Orden).HasDefaultValue(1);
            entity.Property(e => e.Url)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductosImagenes)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductosImagenes_Productos");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC0713DBC2AF");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Roles_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF1BF7A26A").IsUnique();

            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tallas__3214EC0710782118");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Tallas_Estatus", "[Estatus] IN (0,1)"));

            entity.HasIndex(e => e.Numero, "UQ__Tallas__7E532BC6A265B055").IsUnique();

            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Numero)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsosCupone>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsosCupo__3214EC07BC8598BE");

            entity.HasIndex(e => e.PedidoId, "UQ_UsosCupones_PedidoId").IsUnique();

            entity.Property(e => e.FechaUso)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoDescuentoAplicado).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cupon).WithMany(p => p.UsosCupones)
                .HasForeignKey(d => d.CuponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsosCupones_Cupones");

            entity.HasOne(d => d.Pedido).WithOne(p => p.UsosCupone)
                .HasForeignKey<UsosCupone>(d => d.PedidoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsosCupones_Pedidos");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsosCupones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsosCupones_Usuarios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__2B3DE7B8550B75B7");

            entity.ToTable(tb => tb.HasCheckConstraint("CHK_Usuarios_Estatus", "[Estatus] IN (1,2,3,4)"));

            entity.HasIndex(e => e.Correo, "UQ__Usuario__60695A19B064666A").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Estatus).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}