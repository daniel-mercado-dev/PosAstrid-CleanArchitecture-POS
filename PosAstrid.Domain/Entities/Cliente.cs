using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Entities
{
    public sealed class Cliente : Entity
    {
        public string Nombre { get; private set; } = string.Empty;
        public string TipoDocumento { get; private set; } = string.Empty; 
        public string NumeroDocumento { get; private set; } = string.Empty;
        public string? Direccion { get; private set; }
        public string? Telefono { get; private set; }

        private readonly List<VentaCabecera> _ventas = new();
        public IReadOnlyCollection<VentaCabecera> Ventas => _ventas.AsReadOnly();

        // 1. EL CONSTRUCTOR ES PRIVADO: Nadie puede crear un cliente con 'new' por fuera.
        private Cliente(string nombre, string tipoDocumento, string numeroDocumento, string? direccion, string? telefono)
        {
            Nombre = nombre;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Direccion = direccion;
            Telefono = telefono;
        }

        // Constructor privado vacío para Entity Framework Core
        private Cliente() { }

        // 2. EL MÉTODO ESTÁTICO DE FÁBRICA (El enfoque de tu profesor)
        public static Cliente Crear(string nombre, string tipoDocumento, string numeroDocumento, string? direccion = null, string? telefono = null)
        {
            // AQUÍ ESTÁ EL GRAN PODER: Validaciones de negocio antes de crear el objeto
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            if (tipoDocumento == "DNI" && numeroDocumento.Length != 8)
                throw new ArgumentException("El DNI debe tener exactamente 8 dígitos.");

            if (tipoDocumento == "RUC" && numeroDocumento.Length != 11)
                throw new ArgumentException("El RUC debe tener exactamente 11 dígitos.");

            // Si todas las reglas se cumplen, recién se instancia y se devuelve el objeto
            return new Cliente(nombre, tipoDocumento, numeroDocumento, direccion, telefono);
        }

        public void ActualizarInformacion(string nombre, string? direccion, string? telefono)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}