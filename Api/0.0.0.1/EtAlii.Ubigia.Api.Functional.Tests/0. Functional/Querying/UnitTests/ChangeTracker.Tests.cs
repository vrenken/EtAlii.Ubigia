namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public class ChangeTracker_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ChangeTracker_New()
        {
            // Arrange.

            // Act.
            new ChangeTracker();

            // Assert.
        }
    }
}