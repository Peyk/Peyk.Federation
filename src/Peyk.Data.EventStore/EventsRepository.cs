using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Peyk.Data.Abstractions;
using Peyk.Data.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Peyk.Data.EventStore
{
    public class EventsRepository : IEventsRepository
    {
        private readonly IEventStoreConnection _connection;
        private readonly ILogger _logger;

        public EventsRepository(
            IEventStoreConnection connection,
            ILogger<EventsRepository> logger
        )
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task AppendEventAsync(
            IEvent e,
            CancellationToken cancellationToken = default
        )
        {
            string streamName = StreamNames.GetStreamNameForEventType(e.Type);

            await _connection.ConnectAsync()
                .ConfigureAwait(false);

            string json = JsonConvert.SerializeObject(e);
            byte[] bytes = Encoding.UTF8.GetBytes(json);

            await _connection.AppendToStreamAsync(
                streamName,
                ExpectedVersion.Any,
                new EventData(Guid.NewGuid(), e.Type, true, bytes, null)
            ).ConfigureAwait(false);
        }
    }
}
