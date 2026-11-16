using MediatR;  
using PosAstrid.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace PosAstrid.Application.Features.Ventas.EventHandlers
{
    // Este manejador se ejecuta automáticamente cuando se dispara VentaRegistradaDomainEvent
    public class VentaRegistradaHandler : INotificationHandler<VentaRegistradaDomainEvent>
    {
        public async Task Handle(VentaRegistradaDomainEvent notification, CancellationToken cancellationToken)
        {
            // Aquí puedes hacer lo que necesites de forma limpia y desacoplada:
            // 1. Enviar una alerta de auditoría
            // 2. Imprimir el ticket en la térmica de forma asíncrona
            // 3. Registrar métricas
            
            await Task.CompletedTask;
            System.Console.WriteLine($"[EVENTO RECIBIDO] Se procesó la venta ID: {notification.VentaId} por un total de S/. {notification.MontoTotal}");
        }
    }
}