namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class IndexedComponentTests
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
