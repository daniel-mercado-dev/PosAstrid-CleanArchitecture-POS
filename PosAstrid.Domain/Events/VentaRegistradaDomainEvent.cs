using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Domain.Events
{
    // Hereda de tu IDomainEvent (que a su vez hereda de INotification de MediatR)
    public sealed record VentaRegistradaDomainEvent(int VentaId, decimal MontoTotal, int UsuarioId) : IDomainEvent;
}