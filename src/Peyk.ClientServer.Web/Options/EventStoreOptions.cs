namespace Peyk.ClientServer.Web.Options
{
    /// <summary>
    /// Contains application settings for connecting to an EventStore database
    /// </summary>
    public class EventStoreOptions
    {
        /// <summary>
        /// EventStore connection string
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
