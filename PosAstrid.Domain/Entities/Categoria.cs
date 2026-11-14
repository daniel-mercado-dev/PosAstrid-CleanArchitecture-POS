namespace PosAstrid.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public bool Activo { get; private set; } = true;

        // Colección privada para proteger la integridad de los productos
        private readonly List<Producto> _productos = new();
        public IReadOnlyCollection<Producto> Productos => _productos.AsReadOnly();

        public Categoria(string nombre)
        {
            Nombre = nombre;
            Activo = true;
        }

        protected Categoria() { }

        public void ActualizarNombre(string nuevoNombre)
        {
            Nombre = nuevoNombre;
        }

        public void CambiarEstado(bool estado)
        {
            Activo = estado;
        }
    }
}