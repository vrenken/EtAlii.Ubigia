namespace EtAlii.Ubigia.Infrastructure.Fabric.Tests
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Storage;
    using EtAlii.Ubigia.Storage.InMemory;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public sealed class EntryGetterTests
    {
        private const string TestStorageName = "InMemory Test storage";
        
        private IStorage CreateTestStorage()
        {
            var storageConfiguration = new StorageConfiguration()
                .Use(TestStorageName)
                .UseInMemoryStorage();
            var storage = new StorageFactory().Create(storageConfiguration);
            return storage;
        }
        
        [Fact]
        public void EntryGetter_Get_All()
        {
            // Arrange.
            var storage = CreateTestStorage();

            var entryGetter = new EntryGetter(storage);
            var testIdentifier = new TestIdentifierFactory().Create();

            // Act.
            var item = entryGetter.Get(testIdentifier, EntryRelation.All);

            // Assert.
            Assert.Equal(Identifier.Empty,item.Id);
        }
    }
}
