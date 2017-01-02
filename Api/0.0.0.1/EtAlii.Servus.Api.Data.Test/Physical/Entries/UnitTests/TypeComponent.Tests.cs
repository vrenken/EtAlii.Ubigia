namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeComponent_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TypeComponent_Create()
        {
            // Arrange.

            // Act.
            var typeComponent = new TypeComponent();

            // Assert.
            Assert.IsNotNull(typeComponent);
            Assert.IsNull(typeComponent.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void TypeComponent_Create_With_Type()
        {
            // Arrange.
            const string type = "Test";

            // Act.
            var typeComponent = new TypeComponent { Type = type };

            // Assert.
            Assert.IsNotNull(typeComponent);
            Assert.AreEqual(type, typeComponent.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPart_Empty_Check()
        {
            Assert.IsNotNull(ContentPart.Empty);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ContentPart_Empty_Check_Data()
        {
            Assert.IsNotNull(ContentPart.Empty.Data);
            Assert.AreEqual(0, ContentPart.Empty.Data.Length);
        }
    }
}
