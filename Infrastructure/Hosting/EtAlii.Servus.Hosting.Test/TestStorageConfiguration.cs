namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using System;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Storage;

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