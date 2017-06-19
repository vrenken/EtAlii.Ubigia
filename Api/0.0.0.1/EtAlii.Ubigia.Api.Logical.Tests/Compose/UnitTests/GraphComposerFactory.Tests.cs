namespace EtAlii.Ubigia.Api.Logical.Tests
{
    using EtAlii.Ubigia.Api.Logical;
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
