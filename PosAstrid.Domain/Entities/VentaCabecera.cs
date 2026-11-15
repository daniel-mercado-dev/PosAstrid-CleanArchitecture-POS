namespace PosAstrid.Domain.Entities
{
    public sealed class VentaCabecera
    {
        public int Id { get; private set; }
        public DateTime Fecha { get; private set; } = DateTime.Now;
        public decimal MontoTotal { get; private set; }
        public string MetodoPago { get; private set; } = string.Empty; 

        // 1. Lista privada para proteger los datos de modificaciones externas indebidas
        private readonly List<VentaDetalle> _detalles = new();
        public IReadOnlyCollection<VentaDetalle> Detalles => _detalles.AsReadOnly();

        public int? ClienteId { get; private set; }
        public Cliente? Cliente { get; private set; }

        public int UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }

        // 2. Constructor para inicializar la venta de forma obligatoria
        public VentaCabecera(int usuarioId, string metodoPago, int? clienteId = null)
        {
            UsuarioId = usuarioId;
            MetodoPago = metodoPago;
            ClienteId = clienteId;
            Fecha = DateTime.Now;
        }

        // 3. Constructor protegido requerido por Entity Framework Core
        private VentaCabecera() { }

        // 4. Método de negocio para agregar un detalle de forma segura
        public void AgregarDetalle(int? productoId, string productoNombre, int cantidad, decimal precioUnitario)
        {
            var detalle = new VentaDetalle(productoId, productoNombre, cantidad, precioUnitario);
            _detalles.Add(detalle);
            
            // Recalculamos el total automáticamente cada vez que entra un producto
            CalcularMontoTotal();
        }

        // 5. Lógica interna para calcular el total
        private void CalcularMontoTotal()
        {
            MontoTotal = _detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
        }
    }
}