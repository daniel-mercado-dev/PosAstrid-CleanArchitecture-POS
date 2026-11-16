using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Entities
{
    public sealed class Categoria : Entity
    {
        public string Nombre { get; private set; } = string.Empty;
        public bool Activo { get; private set; } = true;

        // Colección privada para proteger la integridad de los productos
        private readonly List<Producto> _productos = new();
        public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();

        // 1. Constructor principal privado (nadie hace new por fuera)
        private Categoria(string nombre)
        {
            Nombre = nombre;
            Activo = true;
        }

        // 2. Constructor privado requerido por Entity Framework Core
        private Categoria() { }

        // 3. Método Estático de Fábrica (controla la instanciación segura)
        public static Categoria Crear(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            return new Categoria(nombre.Trim());
        }

        // 4. Métodos de negocio controlados
        public void ActualizarNombre(string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(nuevoNombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            Nombre = nuevoNombre.Trim();
        }

        public void CambiarEstado(bool estado)
        {
            Activo = estado;
        }
    }
}