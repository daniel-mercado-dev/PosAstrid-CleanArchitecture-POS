namespace PosAstrid.Api.DTOs;

public record CrearProductoRequest(string Nombre, decimal PrecioBase, int CategoriaId);