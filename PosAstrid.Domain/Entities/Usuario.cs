using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Entities
{
    public sealed class Usuario : Entity    
    {
        public string NombreCompleto { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Rol { get; private set; } = "Cajero"; // Administrador, Cajero, etc.
        public bool Activo { get; private set; } = true;

        // Campo de auditoría muy útil en la vida real
        public DateTime FechaCreacion { get; private set; } = DateTime.UtcNow;

        // Relación con las ventas realizadas por este usuario
        private readonly List<VentaCabecera> _ventas = new();
        public IReadOnlyCollection<VentaCabecera> Ventas => _ventas.AsReadOnly();

        // 1. Constructor para registrar un nuevo usuario obligatoriamente con sus datos base
        private Usuario(string nombreCompleto, string username, string passwordHash, string rol = "Cajero")
        {
            NombreCompleto = nombreCompleto;
            Username = username;
            PasswordHash = passwordHash;
            Rol = rol;
            Activo = true;
            FechaCreacion = DateTime.UtcNow;
        }

        // 2. Constructor protegido obligatorio para que Entity Framework Core lea de la BD
        private Usuario() { }

        public static Usuario Crear(string nombreCompleto, string username, string passwordHash, string rol = "Cajero")
        {
            // Aquí puedes meter validaciones de negocio si lo deseas (ej: username no vacío)
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            return new Usuario(nombreCompleto, username, passwordHash, rol);
        }

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