namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using Xunit;

    public class IndexedComponent_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void IndexedComponent_Create()
        {
            // Arrange.

            // Act.
            var indexedComponent = new IndexedComponent();

            // Assert.
            Assert.NotNull(indexedComponent);
            Assert.Equal(Relation.None, indexedComponent.Relation);
        }
    }
}
