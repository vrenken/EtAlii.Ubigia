namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Persistence;

    public static class TestStorageConfiguration
    {
        public const string TestName = "Unit test storage";

        public static IStorageConfiguration Create()
        {
            return new StorageConfiguration()
                .Use(TestName);
        }
    }
}