namespace EtAlii.Ubigia.Api.Tests.UnitTests
{
    using Xunit;

    public class TypeComponent_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeComponent_Create()
        {
            // Arrange.

            // Act.
            var typeComponent = new TypeComponent();

            // Assert.
            Assert.NotNull(typeComponent);
            Assert.Null(typeComponent.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void TypeComponent_Create_With_Type()
        {
            // Arrange.
            const string type = "Test";

            // Act.
            var typeComponent = new TypeComponent { Type = type };

            // Assert.
            Assert.NotNull(typeComponent);
            Assert.Equal(type, typeComponent.Type);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Empty_Check()
        {
            Assert.NotNull(ContentPart.Empty);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ContentPart_Empty_Check_Data()
        {
            Assert.NotNull(ContentPart.Empty.Data);
            Assert.Equal(0, ContentPart.Empty.Data.Length);
        }
    }
}
