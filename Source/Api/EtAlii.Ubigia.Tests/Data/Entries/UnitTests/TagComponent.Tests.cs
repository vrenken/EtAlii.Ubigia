namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class TagComponentTests
    {
        [Fact]
        public void TagComponent_Create()
        {
            // Arrange.

            // Act.
            var tagComponent = new TagComponent();

            // Assert.
            Assert.NotNull(tagComponent);
            Assert.Null(tagComponent.Tag);
        }

        [Fact]
        public void TagComponent_Create_With_Type()
        {
            // Arrange.
            const string tag = "Test";

            // Act.
            var tagComponent = new TagComponent { Tag = tag };

            // Assert.
            Assert.NotNull(tagComponent);
            Assert.Equal(tag, tagComponent.Tag);
        }
    }
}
