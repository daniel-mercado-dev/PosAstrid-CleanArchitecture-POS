using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Entities
{
    public sealed class Inventario : Entity
    {
        public int ProductoId { get; private set; }
        public Producto? Producto { get; private set; }

        public int StockInicial { get; private set; }
        public int StockFinal { get; private set; }

        // Calculada en la base de datos (ver InventarioConfiguration)
        public int Vendido { get; private set; }

        public decimal PrecioUnitario { get; private set; }

        // Calculada en la base de datos (ver InventarioConfiguration)
        public decimal MontoTotal { get; private set; }

        public DateTime Fecha { get; private set; }

        private Inventario() { }

        private Inventario(int productoId, int stockInicial, int stockFinal, decimal precioUnitario, DateTime fecha)
        {
            ProductoId = productoId;
            StockInicial = stockInicial;
            StockFinal = stockFinal;
            PrecioUnitario = precioUnitario;
            Fecha = fecha;
        }

        public static Inventario Crear(int productoId, int stockInicial, int stockFinal, decimal precioUnitario, DateTime fecha)
        {
            if (productoId <= 0)
                throw new ArgumentException("Debe asociar el inventario a un producto válido.");

            if (stockInicial < 0 || stockFinal < 0)
                throw new ArgumentException("Los valores de stock no pueden ser negativos.");

            if (stockFinal > stockInicial)
                throw new ArgumentException("El stock final no puede ser mayor que el stock inicial.");

            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            if (fecha.Kind != DateTimeKind.Utc)
                throw new ArgumentException("La fecha debe estar en UTC.");

            return new Inventario(productoId, stockInicial, stockFinal, precioUnitario, fecha);
        }

        public void ActualizarStockFinal(int stockFinal)
        {
            if (stockFinal < 0)
                throw new ArgumentException("El stock final no puede ser negativo.");

            if (stockFinal > StockInicial)
                throw new ArgumentException("El stock final no puede ser mayor que el stock inicial.");

            StockFinal = stockFinal;
        }

        public void ActualizarPrecio(decimal precioUnitario)
        {
            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");

            PrecioUnitario = precioUnitario;
        }
    }
}