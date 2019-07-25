namespace EtAlii.Ubigia.Api.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    public class Cache_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Cache_Create()
        {
            // Arrange.

            // Act.
            var cache = new Cache(true);

            // Assert.
            Assert.NotNull(cache);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task Cache_Create_Null()
        {
            // Arrange.
            var identifier = new TestIdentifierFactory().Create();
            var cache = new Cache(true);

            // Act.
            var entry = await cache.GetEntry(identifier, () => Task.FromResult<IReadOnlyEntry>(null));

            // Assert.
            Assert.Null(entry);
        }

    }
}
