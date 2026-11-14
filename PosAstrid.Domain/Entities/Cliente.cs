namespace PosAstrid.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string TipoDocumento { get; private set; } = string.Empty; // DNI / RUC
        public string NumeroDocumento { get; private set; } = string.Empty;
        public string? Direccion { get; private set; }
        public string? Telefono { get; private set; }

        private readonly List<VentaCabecera> _ventas = new();
        public IReadOnlyCollection<VentaCabecera> Ventas => _ventas.AsReadOnly();

        public Cliente(string nombre, string tipoDocumento, string numeroDocumento, string? direccion = null, string? telefono = null)
        {
            Nombre = nombre;
            TipoDocumento = tipoDocumento;
            NumeroDocumento = numeroDocumento;
            Direccion = direccion;
            Telefono = telefono;
        }

        protected Cliente() { }

        public void ActualizarInformacion(string nombre, string? direccion, string? telefono)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}