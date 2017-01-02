namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class EntryCacheHelper_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void EntryCacheHelper_Create()
        {
            // Arrange.
            var cacheProvider = new EntryCacheProvider();

            // Act.
            var helper = new EntryCacheHelper(cacheProvider);

            // Assert.
            Assert.NotNull(helper);
        }
    }
}
