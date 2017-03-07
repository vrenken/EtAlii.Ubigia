namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using EtAlii.Ubigia.Api.Fabric;
    using Xunit;

    public class ContentCacheHelper_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentCacheHelper_Create()
        {
            // Arrange.
            var cacheProvider = new ContentCacheProvider();

            // Act.
            var helper = new ContentCacheHelper(cacheProvider);

            // Assert.
            Assert.NotNull(helper);
        }
    }
}
