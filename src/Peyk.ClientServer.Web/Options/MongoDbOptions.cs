namespace Peyk.ClientServer.Web.Options
{
    /// <summary>
    /// Contains application settings for connecting to a MongoDB database
    /// </summary>
    public class MongoDbOptions
    {
        /// <summary>
        /// MongoDB connection string
        /// </summary>
        public string ConnectionString { get; set; }
    }
}