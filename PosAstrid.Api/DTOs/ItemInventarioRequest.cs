namespace PosAstrid.Api.DTOs;

public record ItemInventarioRequest(int ProductoId, int StockInicial, int StockFinal, decimal PrecioUnitario);

public record RegistrarInventarioRequest(List<ItemInventarioRequest> Items);