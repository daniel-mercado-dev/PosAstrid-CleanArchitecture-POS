using PosAstrid.Domain.Entities;

namespace PosAstrid.Domain.Abstractions;

public sealed record TicketPrintJob(int VentaId);

public interface ITicketPrintQueue
{
    ValueTask EncolarAsync(TicketPrintJob job, CancellationToken ct = default);
}

public interface IImpresoraTermica
{
    Task ImprimirAsync(VentaCabecera venta, CancellationToken ct = default);
}
