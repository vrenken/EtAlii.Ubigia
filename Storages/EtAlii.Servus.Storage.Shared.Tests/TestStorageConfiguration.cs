namespace EtAlii.Servus.Storage.Tests
{
    using EtAlii.Servus.Storage;


    public class TestStorageConfiguration : IStorageConfiguration
    {
        public string Name { get; set; }

        public static TestStorageConfiguration Create()
        {
            return new TestStorageConfiguration
            {
                Name = "Unit test storage"
            };
        }
    }
}
