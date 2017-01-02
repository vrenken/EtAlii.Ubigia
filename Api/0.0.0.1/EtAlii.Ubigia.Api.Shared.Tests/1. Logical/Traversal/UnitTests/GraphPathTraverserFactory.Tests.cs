namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class GraphPathTraverserFactory_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathTraverserFactory_New()
        {
            // Arrange.

            // Act.
            var factory = new GraphPathTraverserFactory();

            // Assert.
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphPathTraverserFactory_Create()
        {
            // Arrange.
            var factory = new GraphPathTraverserFactory();
            var configuration = new GraphPathTraverserConfiguration();

            // Act.
            var traverser = factory.Create(configuration);

            // Assert.
            Assert.NotNull(traverser);
        }
    }
}
