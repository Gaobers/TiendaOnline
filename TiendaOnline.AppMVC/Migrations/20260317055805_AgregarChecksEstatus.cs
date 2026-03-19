using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaOnline.AppMVC.Migrations
{
    public partial class AgregarChecksEstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHK_Categorias_Estatus",
                table: "Categorias",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Colores_Estatus",
                table: "Colores",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Cupones_Estatus",
                table: "Cupones",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_DireccionesUsuarios_Estatus",
                table: "DireccionesUsuarios",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_EstadosPedidos_Estatus",
                table: "EstadosPedidos",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Marcas_Estatus",
                table: "Marcas",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_MetodosEnvio_Estatus",
                table: "MetodosEnvio",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_MetodosPago_Estatus",
                table: "MetodosPago",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Productos_Estatus",
                table: "Productos",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Roles_Estatus",
                table: "Roles",
                sql: "[Estatus] IN (0,1)");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_Tallas_Estatus",
                table: "Tallas",
                sql: "[Estatus] IN (0,1)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_Categorias_Estatus",
                table: "Categorias");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Colores_Estatus",
                table: "Colores");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Cupones_Estatus",
                table: "Cupones");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_DireccionesUsuarios_Estatus",
                table: "DireccionesUsuarios");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_EstadosPedidos_Estatus",
                table: "EstadosPedidos");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Marcas_Estatus",
                table: "Marcas");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_MetodosEnvio_Estatus",
                table: "MetodosEnvio");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_MetodosPago_Estatus",
                table: "MetodosPago");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Productos_Estatus",
                table: "Productos");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Roles_Estatus",
                table: "Roles");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_Tallas_Estatus",
                table: "Tallas");

        }
    }
}