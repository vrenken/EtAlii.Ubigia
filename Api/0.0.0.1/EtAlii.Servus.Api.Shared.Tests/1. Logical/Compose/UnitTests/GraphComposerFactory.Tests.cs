namespace EtAlii.Servus.Api.Logical.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Xunit;

    
    public class GraphComposerFactory_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void GraphComposerFactory_New()
        {
            // Arrange.
            var traverserFactory = new GraphPathTraverserFactory();

            // Act.
            var factory = new GraphComposerFactory(traverserFactory);

            // Assert.
        }
    }
}
