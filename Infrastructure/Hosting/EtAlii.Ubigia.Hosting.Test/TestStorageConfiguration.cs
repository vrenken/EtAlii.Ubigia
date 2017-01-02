namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using System;
    using EtAlii.Ubigia.Infrastructure;
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