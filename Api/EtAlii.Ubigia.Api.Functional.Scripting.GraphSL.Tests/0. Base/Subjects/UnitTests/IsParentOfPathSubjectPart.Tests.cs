namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using Xunit;

    public class ParentPathSubjectPartTests
    {

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ParentPathSubjectPart_ToString()
        {
            // Arrange.
            var part = new ParentPathSubjectPart();

            // Act.
            var result = part.ToString();

            // Assert.
            Assert.Equal("/", result);
        }
    }
}