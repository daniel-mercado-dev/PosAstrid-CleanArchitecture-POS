using Microsoft.Extensions.Logging;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Entities;

namespace PosAstrid.Infrastructure.Printing;

public sealed class ImpresoraSimulada : IImpresoraTermica
{
    private readonly ILogger<ImpresoraSimulada> _log;
    public ImpresoraSimulada(ILogger<ImpresoraSimulada> log) => _log = log;

    public async Task ImprimirAsync(VentaCabecera venta, CancellationToken ct = default)
    {
        await Task.Delay(3000, ct); // simula una impresora lenta
        _log.LogInformation("[IMPRESO] Venta {Id} total S/ {Total}", venta.Id, venta.MontoTotal);
    }
}