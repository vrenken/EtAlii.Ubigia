namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class NodeSetTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void NodeSet_New()
        {
            // Arrange.

            // Act.
            var nodeSet = new NodeSet(null, null, null);

            // Assert.
            Assert.NotNull(nodeSet);
        }
    }
}