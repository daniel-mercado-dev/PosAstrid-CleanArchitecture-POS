using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Entities
{
    public sealed class Producto : Entity
    {
        public string Nombre { get; private set; } = string.Empty;
        public decimal PrecioBase { get; private set; }

        public int CategoriaId { get; private set; }
        public Categoria? Categoria { get; private set; }

        private Producto(string nombre, decimal precioBase, int categoriaId)
        {
            Nombre = nombre;
            PrecioBase = precioBase;
            CategoriaId = categoriaId;
        }

        private Producto() { }

        public static Producto Crear(string nombre, decimal precioBase, int categoriaId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (precioBase < 0)
                throw new ArgumentException("El precio base no puede ser negativo.");

            if (categoriaId <= 0)
                throw new ArgumentException("Debe asociar el producto a una categoría válida.");

            return new Producto(nombre, precioBase, categoriaId);
        }

        public void ActualizarDatos(string nombre, decimal precioBase, int categoriaId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (precioBase < 0)
                throw new ArgumentException("El precio base no puede ser negativo.");

            Nombre = nombre;
            PrecioBase = precioBase;
            CategoriaId = categoriaId;
        }
    }
}