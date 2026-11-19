using PosAstrid.Domain.Abstractions;
namespace PosAstrid.Domain.Entities
{
    public class VentaDetalle : Entity
    {
        public int VentaCabeceraId { get; private set; }
        public VentaCabecera? Cabecera { get; private set; }

        public int? ProductoId { get; private set; }
        public Producto? Producto { get; private set; }

        public string ProductoNombre { get; private set; } = string.Empty;
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }

        // Constructor para obligar a pasar los datos requeridos al crear el detalle
        internal VentaDetalle(int? productoId, string productoNombre, int cantidad, decimal precioUnitario)
        {
            if (string.IsNullOrWhiteSpace(productoNombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (precioUnitario < 0)
                throw new ArgumentException("El precio unitario no puede ser negativo.");
                
            ProductoId = productoId;
            ProductoNombre = productoNombre;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        

        // Constructor protegido requerido por Entity Framework Core para poder reconstruir el objeto desde la BD
        private VentaDetalle() { }
    }
}