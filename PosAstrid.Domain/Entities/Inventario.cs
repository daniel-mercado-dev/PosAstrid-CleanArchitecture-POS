using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosAstrid.Domain.Entities
{
    public class Inventario
    {
        [Key]
        public int Id { get; private set; }

        public int ProductoId { get; private set; }
        public Producto? Producto { get; private set; }

        public int StockInicial { get; private set; }
        public int StockFinal { get; private set; }

        // SQL Server calcula esta columna automáticamente (StockInicial - StockFinal)
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Vendido { get; private set; }

        public decimal PrecioUnitario { get; private set; }

        // SQL Server calcula esta columna automáticamente (Vendido * PrecioUnitario)
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal MontoTotal { get; private set; }

        public DateTime Fecha { get; private set; }

        // 1. Constructor para registrar un control de inventario inicial
        public Inventario(int productoId, int stockInicial, int stockFinal, decimal precioUnitario)
        {
            ProductoId = productoId;
            StockInicial = stockInicial;
            StockFinal = stockFinal;
            PrecioUnitario = precioUnitario;
            Fecha = DateTime.Now;
        }

        // 2. Constructor protegido obligatorio para Entity Framework Core
        protected Inventario() { }

        // 3. Método de negocio para actualizar el stock final o registrar el cierre del día
        public void ActualizarStockFinal(int stockFinal)
        {
            StockFinal = stockFinal;
        }
    }
}