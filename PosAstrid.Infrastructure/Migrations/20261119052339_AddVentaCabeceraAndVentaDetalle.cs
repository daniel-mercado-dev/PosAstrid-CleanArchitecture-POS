using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosAstrid.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVentaCabeceraAndVentaDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioDiario_Productos_ProductoId",
                table: "InventarioDiario");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaCabeceras_Cliente_ClienteId",
                table: "VentaCabeceras");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaCabeceras_Usuario_UsuarioId",
                table: "VentaCabeceras");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalles_Productos_ProductoId",
                table: "VentaDetalles");

            migrationBuilder.AlterColumn<string>(
                name: "ProductoNombre",
                table: "VentaDetalles",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                table: "VentaDetalles",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontoTotal",
                table: "VentaCabeceras",
                type: "numeric(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "MetodoPago",
                table: "VentaCabeceras",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categoria",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioDiario_Productos_ProductoId",
                table: "InventarioDiario",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaCabeceras_Cliente_ClienteId",
                table: "VentaCabeceras",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaCabeceras_Usuario_UsuarioId",
                table: "VentaCabeceras",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalles_Productos_ProductoId",
                table: "VentaDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventarioDiario_Productos_ProductoId",
                table: "InventarioDiario");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaCabeceras_Cliente_ClienteId",
                table: "VentaCabeceras");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaCabeceras_Usuario_UsuarioId",
                table: "VentaCabeceras");

            migrationBuilder.DropForeignKey(
                name: "FK_VentaDetalles_Productos_ProductoId",
                table: "VentaDetalles");

            migrationBuilder.AlterColumn<string>(
                name: "ProductoNombre",
                table: "VentaDetalles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioUnitario",
                table: "VentaDetalles",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MontoTotal",
                table: "VentaCabeceras",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MetodoPago",
                table: "VentaCabeceras",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Categoria",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_InventarioDiario_Productos_ProductoId",
                table: "InventarioDiario",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaCabeceras_Cliente_ClienteId",
                table: "VentaCabeceras",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VentaCabeceras_Usuario_UsuarioId",
                table: "VentaCabeceras",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VentaDetalles_Productos_ProductoId",
                table: "VentaDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id");
        }
    }
}
