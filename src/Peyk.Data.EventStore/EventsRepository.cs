using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Peyk.Data.Abstractions;
using Peyk.Data.Events;
using Peyk.Data.EventStore.Types;

namespace Peyk.Data.EventStore
{
    public class EventsRepository : IEventsRepository
    {
        private readonly ConnectionConfigs _connectionConfigs;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public EventsRepository(
            ConnectionConfigs connectionConfigs,
            ILogger<EventsRepository> logger
        )
        {
            _connectionConfigs = connectionConfigs;
            _logger = logger;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_connectionConfigs.Url, UriKind.Absolute)
            };
            _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(
                $"Basic {_connectionConfigs.User}:{_connectionConfigs.Password}"
            );
            _httpClient.DefaultRequestHeaders.Accept.Add(
                MediaTypeWithQualityHeaderValue.Parse("application/vnd.eventstore.atom+json")
            );
        }

        public async Task AppendEventAsync(
            IEvent e,
            CancellationToken cancellationToken = default
        )
        {
            string streamName = StreamNames.GetStreamNameForEventType(e.Type);
            var req = new HttpRequestMessage(HttpMethod.Post, $"streams/{Uri.EscapeDataString(streamName)}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json")
            };
            req.Headers.Add("ES-EventType", e.Type);
            req.Headers.Add("ES-EventId", Guid.NewGuid().ToString());

            var resp = await _httpClient.SendAsync(req, cancellationToken)
                .ConfigureAwait(false);
            resp.EnsureSuccessStatusCode();
        }
    }
}
