namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore.Tests
{
    using EtAlii.Ubigia.Storage;

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