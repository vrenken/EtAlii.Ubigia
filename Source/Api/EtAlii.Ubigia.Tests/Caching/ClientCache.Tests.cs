namespace EtAlii.Ubigia.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    [CorrelateUnitTests]
    public class ClientCacheTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Cache_Create()
        {
            // Arrange.

            // Act.
            var cache = new ClientCache();

            // Assert.
            Assert.NotNull(cache);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Cache_Create_Null()
        {
            // Arrange.
            var identifier = new TestIdentifierFactory().Create();
            var cache = new ClientCache();

            // Act.
            var entry = await cache.GetEntry(identifier, () => Task.FromResult<IReadOnlyEntry>(null)).ConfigureAwait(false);

            // Assert.
            Assert.Null(entry);
        }

    }
}
