namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;


    public class NodeSetTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeSet_New()
        {
            // Arrange.

            // Act.
            new NodeSet(null, null, null);

            // Assert.
        }
    }
}