namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IndexedComponent_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void IndexedComponent_Create()
        {
            // Arrange.

            // Act.
            var indexedComponent = new IndexedComponent();

            // Assert.
            Assert.IsNotNull(indexedComponent);
            Assert.AreEqual(Relation.None, indexedComponent.Relation);
        }
    }
}
