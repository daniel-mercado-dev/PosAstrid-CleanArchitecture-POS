namespace PosAstrid.Domain.Entities
{
    public sealed class Usuario
    {
        public int Id { get; private set; }
        public string NombreCompleto { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Rol { get; private set; } = "Cajero"; // Administrador, Cajero, etc.
        public bool Activo { get; private set; } = true;

        // Campo de auditoría muy útil en la vida real
        public DateTime FechaCreacion { get; private set; } = DateTime.Now;

        // Relación con las ventas realizadas por este usuario
        private readonly List<VentaCabecera> _ventas = new();
        public IReadOnlyCollection<VentaCabecera> Ventas => _ventas.AsReadOnly();

        // 1. Constructor para registrar un nuevo usuario obligatoriamente con sus datos base
        public Usuario(string nombreCompleto, string username, string passwordHash, string rol = "Cajero")
        {
            NombreCompleto = nombreCompleto;
            Username = username;
            PasswordHash = passwordHash;
            Rol = rol;
            Activo = true;
            FechaCreacion = DateTime.Now;
        }

        // 2. Constructor protegido obligatorio para que Entity Framework Core lea de la BD
        private Usuario() { }

        // 3. Métodos de negocio controlados (en lugar de usar setters libres)
        public void ActualizarInformacion(string nombreCompleto, string rol)
        {
            NombreCompleto = nombreCompleto;
            Rol = rol;
        }

        public void CambiarPassword(string nuevoPasswordHash)
        {
            PasswordHash = nuevoPasswordHash;
        }

        public void CambiarEstado(bool estado)
        {
            Activo = estado; // Permite dar de baja (desactivar) a un cajero sin borrarlo
        }
    }
}