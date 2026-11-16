using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Events;
namespace PosAstrid.Domain.Entities
{
    public sealed class VentaCabecera : Entity

    {
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
        private VentaCabecera(int usuarioId, string metodoPago, int? clienteId = null)
        {
            UsuarioId = usuarioId;
            MetodoPago = metodoPago;
            ClienteId = clienteId;
            Fecha = DateTime.Now;
        }

        // 3. Constructor protegido requerido por Entity Framework Core
        private VentaCabecera() { }

        public static VentaCabecera Crear(int usuarioId, string metodoPago, int? clienteId = null)
{
    if (usuarioId <= 0)
        throw new ArgumentException("El ID del usuario es obligatorio para registrar la venta.");

    if (string.IsNullOrWhiteSpace(metodoPago))
        throw new ArgumentException("El método de pago es obligatorio.");

    var venta = new VentaCabecera(usuarioId, metodoPago, clienteId);

    // 🔥 AQUÍ DISPARAMOS EL EVENTO DE DOMINIO
    venta.RaiseDomainEvent(new VentaRegistradaDomainEvent(venta.Id, venta.MontoTotal, usuarioId));

    return venta;
}

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