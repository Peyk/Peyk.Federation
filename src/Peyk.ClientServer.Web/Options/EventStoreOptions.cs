namespace Peyk.ClientServer.Web.Options
{
    /// <summary>
    /// Contains application settings for connecting to an EventStore database
    /// </summary>
    public class EventStoreOptions
    {
        public string Url { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
