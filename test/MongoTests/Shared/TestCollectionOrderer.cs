using Framework;

namespace MongoTests.Shared
{
    public class TestCollectionOrderer : TestCollectionOrdererBase
    {
        private static readonly string[] Collections =
        {
            "public rooms repository",
        };

        public TestCollectionOrderer()
            : base(Collections)
        {
        }
    }
}