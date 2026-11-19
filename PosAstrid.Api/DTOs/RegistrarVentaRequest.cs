namespace PosAstrid.Api.DTOs;

public record ItemVentaRequest(int? ProductoId, string Producto, int Cantidad, decimal Precio);

public record RegistrarVentaRequest(int UsuarioId, string MetodoPago, int? ClienteId, List<ItemVentaRequest> Items);