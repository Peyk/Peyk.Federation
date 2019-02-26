namespace Peyk.ClientServer.Ops.Query.Models
{
    public struct PublicRoomsFilter
    {
        public int Limit { get; set; }

        public string Since { get; set; }

        public string Server { get; set; }
    }
}