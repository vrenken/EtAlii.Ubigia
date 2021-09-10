namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class TypeComponentTests
    {
        [Fact]
        public void TypeComponent_Create()
        {
            // Arrange.

            // Act.
            var typeComponent = new TypeComponent();

            // Assert.
            Assert.NotNull(typeComponent);
            Assert.Null(typeComponent.Type);
        }

        [Fact]
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

        [Fact]
        public void ContentPart_Empty_Check()
        {
            Assert.NotNull(ContentPart.Empty);
        }

        [Fact]
        public void ContentPart_Empty_Check_Data()
        {
            Assert.NotNull(ContentPart.Empty.Data);
            Assert.Empty(ContentPart.Empty.Data);
        }
    }
}
