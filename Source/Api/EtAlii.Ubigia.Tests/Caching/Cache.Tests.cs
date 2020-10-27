namespace EtAlii.Ubigia.Tests
{
    using System.Threading.Tasks;
    using Xunit;

    public class CacheTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Cache_Create()
        {
            // Arrange.

            // Act.
            var cache = new Cache();

            // Assert.
            Assert.NotNull(cache);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Cache_Create_Null()
        {
            // Arrange.
            var identifier = new TestIdentifierFactory().Create();
            var cache = new Cache();

            // Act.
            var entry = await cache.GetEntry(identifier, () => Task.FromResult<IReadOnlyEntry>(null));

            // Assert.
            Assert.Null(entry);
        }

    }
}
