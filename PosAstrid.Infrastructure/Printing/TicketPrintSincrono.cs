using Microsoft.Extensions.DependencyInjection;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Repositories;

namespace PosAstrid.Infrastructure.Printing;

// Implementa el mismo contrato que la cola, pero imprime AQUÍ, dentro del request HTTP.
public sealed class TicketPrintSincrono : ITicketPrintQueue
{
    private readonly IServiceScopeFactory _scopes;

    public TicketPrintSincrono(IServiceScopeFactory scopes) => _scopes = scopes;

    public async ValueTask EncolarAsync(TicketPrintJob job, CancellationToken ct = default)
    {
        using var scope = _scopes.CreateScope();
        var ventas = scope.ServiceProvider.GetRequiredService<IVentaRepository>();
        var impresora = scope.ServiceProvider.GetRequiredService<IImpresoraTermica>();

        var venta = await ventas.GetByIdAsync(job.VentaId, ct);
        if (venta is not null)
            await impresora.ImprimirAsync(venta, ct);   // aquí se espera los 3 s
    }
}