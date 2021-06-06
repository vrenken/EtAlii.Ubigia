namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using Xunit;

    public class ConstantPathSubjectPartTests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ConstantPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new ConstantPathSubjectPart("Test");

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("Test", result);
        }
    }
}
