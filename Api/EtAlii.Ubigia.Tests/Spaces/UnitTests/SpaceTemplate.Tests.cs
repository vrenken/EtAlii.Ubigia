namespace EtAlii.Ubigia.Tests
{
    using Xunit;

    public class SpaceTemplateTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void SpaceTemplate_Get_All()
        {
            // Arrange.

            // Act.
            var all = SpaceTemplate.All;

            // Assert.
            Assert.NotEmpty(all);
        }
    }
}
