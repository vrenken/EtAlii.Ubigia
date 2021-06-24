// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.Ubigia.Persistence.InMemory;
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
        public async Task EntryGetter_Get_All()
        {
            // Arrange.
            var storage = CreateTestStorage();

            var entryGetter = new EntryGetter(storage);
            var testIdentifier = new TestIdentifierFactory().Create();

            // Act.
            var item = await entryGetter.Get(testIdentifier, EntryRelation.All).ConfigureAwait(false);

            // Assert.
            Assert.Equal(Identifier.Empty,item.Id);
        }
    }
}
