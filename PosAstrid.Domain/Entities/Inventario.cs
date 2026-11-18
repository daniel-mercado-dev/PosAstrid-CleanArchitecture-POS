using System.ComponentModel.DataAnnotations.Schema;
using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Entities
{
    public sealed class Inventario : Entity
    {
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

        // 1. Constructor privado (nadie hace new por fuera)
        private Inventario(int productoId, int stockInicial, int stockFinal, decimal precioUnitario)
        {
            ProductoId = productoId;
            StockInicial = stockInicial;
            StockFinal = stockFinal;
            PrecioUnitario = precioUnitario;
            Fecha = DateTime.UtcNow;
        }

        // 2. Constructor privado requerido por Entity Framework Core
        private Inventario() { }

        // 3. Método Estático de Fábrica (controla la instanciación y validaciones)
        public static Inventario Crear(int productoId, int stockInicial, int stockFinal, decimal precioUnitario)
        {
            if (productoId <= 0)
                throw new ArgumentException("Debe asociar el inventario a un producto válido.");

            if (stockInicial < 0 || stockFinal < 0)
                throw new ArgumentException("Los valores de stock no pueden ser negativos.");

            if (stockFinal > stockInicial)
                throw new ArgumentException("El stock final no puede ser mayor que el stock inicial.");

            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            return new Inventario(productoId, stockInicial, stockFinal, precioUnitario);
        }

        // 4. Método de negocio para actualizar el stock final con validación
        public void ActualizarStockFinal(int stockFinal)
        {
            if (stockFinal < 0)
                throw new ArgumentException("El stock final no puede ser negativo.");

            if (stockFinal > StockInicial)
                throw new ArgumentException("El stock final no puede ser mayor que el stock inicial.");

            StockFinal = stockFinal;
        }
    }
}