using System.Threading.Channels;
using PosAstrid.Domain.Abstractions;

namespace PosAstrid.Infrastructure.Printing;

public sealed class TicketPrintQueue : ITicketPrintQueue
{
    private readonly Channel<TicketPrintJob> _channel =
        Channel.CreateBounded<TicketPrintJob>(new BoundedChannelOptions(200)
        {
            FullMode = BoundedChannelFullMode.Wait
        });

    public ChannelReader<TicketPrintJob> Reader => _channel.Reader;

    public ValueTask EncolarAsync(TicketPrintJob job, CancellationToken ct = default)
        => _channel.Writer.WriteAsync(job, ct);
}