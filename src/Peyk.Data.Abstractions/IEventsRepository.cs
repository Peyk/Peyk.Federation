using System.Threading;
using System.Threading.Tasks;
using Peyk.Data.Events;

namespace Peyk.Data.Abstractions
{
    public interface IEventsRepository
    {
        Task AppendEventAsync(
            IEvent e,
            CancellationToken cancellationToken = default
        );
    }
}
