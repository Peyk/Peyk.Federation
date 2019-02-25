using MongoDB.Bson.Serialization;
using NUlid;

namespace Peyk.Data.Mongo
{
    internal class UlidIdGenerator : IIdGenerator
    {
        public static UlidIdGenerator Instance => _instance;

        private static readonly UlidIdGenerator _instance = new UlidIdGenerator();

        public object GenerateId(object container, object document) =>
            Ulid.NewUlid().ToString();

        public bool IsEmpty(object id) =>
            string.IsNullOrEmpty(id as string);
    }
}