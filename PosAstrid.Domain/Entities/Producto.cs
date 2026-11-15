namespace PosAstrid.Domain.Entities
{
    public sealed class Producto
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public decimal PrecioBase { get; private set; }

        public int CategoriaId { get; private set; }
        public Categoria? Categoria { get; private set; }

        public Producto(string nombre, decimal precioBase, int categoriaId)
        {
            Nombre = nombre;
            PrecioBase = precioBase;
            CategoriaId = categoriaId;
        }

        private Producto() { }

        public void ActualizarDatos(string nombre, decimal precioBase, int categoriaId)
        {
            Nombre = nombre;
            PrecioBase = precioBase;
            CategoriaId = categoriaId;
        }
    }
}