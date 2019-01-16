namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;


    public class ChangeTrackerTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ChangeTracker_New()
        {
            // Arrange.

            // Act.
            var changeTracker = new ChangeTracker();

            // Assert.
            Assert.NotNull(changeTracker);
        }
    }
}