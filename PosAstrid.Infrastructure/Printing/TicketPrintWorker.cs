using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Repositories;

namespace PosAstrid.Infrastructure.Printing;

public sealed class TicketPrintWorker : BackgroundService
{
    private readonly TicketPrintQueue _queue;
    private readonly IServiceScopeFactory _scopes;
    private readonly ILogger<TicketPrintWorker> _log;

    public TicketPrintWorker(TicketPrintQueue queue, IServiceScopeFactory scopes, ILogger<TicketPrintWorker> log)
    {
        _queue = queue;
        _scopes = scopes;
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var job in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                // El worker es singleton: necesita su propio scope para usar servicios scoped
                using var scope = _scopes.CreateScope();
                var ventas = scope.ServiceProvider.GetRequiredService<IVentaRepository>();
                var impresora = scope.ServiceProvider.GetRequiredService<IImpresoraTermica>();

                var venta = await ventas.GetByIdAsync(job.VentaId, stoppingToken);
                if (venta is null)
                {
                    _log.LogWarning("Venta {Id} no encontrada para imprimir.", job.VentaId);
                    continue;
                }

                await impresora.ImprimirAsync(venta, stoppingToken);
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                _log.LogError(ex, "Falló la impresión de la venta {Id}.", job.VentaId);
            }
        }
    }
}