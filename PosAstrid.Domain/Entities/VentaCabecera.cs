using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Events;
namespace PosAstrid.Domain.Entities
{
    public sealed class VentaCabecera : Entity

    {
        public DateTime Fecha { get; private set; } = DateTime.UtcNow;
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
          MetodoPago = metodoPago.Trim();
            ClienteId = clienteId;
            Fecha = DateTime.UtcNow;
        }

        // 3. Constructor protegido requerido por Entity Framework Core
        private VentaCabecera() { }

        public void MarcarRegistrada()
        {
            RaiseDomainEvent(new VentaRegistradaDomainEvent(Id, MontoTotal, UsuarioId));
        }

       public static VentaCabecera Crear(int usuarioId, string metodoPago, int? clienteId = null)
{
    if (usuarioId <= 0)
        throw new ArgumentException("El ID del usuario es obligatorio para registrar la venta.");

    if (string.IsNullOrWhiteSpace(metodoPago))
        throw new ArgumentException("El método de pago es obligatorio.");

    return new VentaCabecera(usuarioId, metodoPago, clienteId);
}

public void Confirmar()
{
    if (_detalles.Count == 0)
        throw new InvalidOperationException("La venta debe tener al menos un detalle.");

    RaiseDomainEvent(new VentaRegistradaDomainEvent(Id, MontoTotal, UsuarioId));
}

        // 4. Método de negocio para agregar un detalle de forma segura
        public void AgregarDetalle(int? productoId, string productoNombre, int cantidad, decimal precioUnitario)
{
    if (string.IsNullOrWhiteSpace(productoNombre))
        throw new ArgumentException("El nombre del producto es obligatorio.");

    if (cantidad <= 0)
        throw new ArgumentException("La cantidad debe ser mayor que cero.");

    if (precioUnitario < 0)
        throw new ArgumentException("El precio unitario no puede ser negativo.");

    var detalle = new VentaDetalle(productoId, productoNombre.Trim(), cantidad, precioUnitario);
    _detalles.Add(detalle);

    CalcularMontoTotal();
}

        // 5. Lógica interna para calcular el total
        private void CalcularMontoTotal()
        {
            MontoTotal = _detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
        }
    }
}