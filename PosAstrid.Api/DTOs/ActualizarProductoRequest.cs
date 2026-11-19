namespace PosAstrid.Api.DTOs;

public record ActualizarProductoRequest(string Nombre, decimal PrecioBase, int CategoriaId);