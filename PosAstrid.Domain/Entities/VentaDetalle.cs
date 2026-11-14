namespace PosAstrid.Domain.Entities
{
    public class VentaDetalle
    {
        public int Id { get; private set; }
        public int VentaCabeceraId { get; private set; }
        public VentaCabecera? Cabecera { get; private set; }

        public int? ProductoId { get; private set; }
        public Producto? Producto { get; private set; }

        public string ProductoNombre { get; private set; } = string.Empty;
        public int Cantidad { get; private set; }
        public decimal PrecioUnitario { get; private set; }

        // Constructor para obligar a pasar los datos requeridos al crear el detalle
        public VentaDetalle(int? productoId, string productoNombre, int cantidad, decimal precioUnitario)
        {
            ProductoId = productoId;
            ProductoNombre = productoNombre;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        // Constructor protegido requerido por Entity Framework Core para poder reconstruir el objeto desde la BD
        protected VentaDetalle() { }
    }
}