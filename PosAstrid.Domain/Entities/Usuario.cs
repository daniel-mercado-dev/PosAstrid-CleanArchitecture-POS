namespace PosAstrid.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Rol { get; set; } = "Cajero"; // Administrador, Cajero, etc.
        public bool Activo { get; set; } = true;

        public List<VentaCabecera> Ventas { get; set; } = new List<VentaCabecera>();
    }
}