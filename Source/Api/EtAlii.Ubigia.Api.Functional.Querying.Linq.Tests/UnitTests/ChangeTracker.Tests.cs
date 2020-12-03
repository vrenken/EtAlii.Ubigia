namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class ChangeTrackerTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ChangeTracker_New()
        {
            // Arrange.

            // Act.
            using var changeTracker = new ChangeTracker();

            // Assert.
            Assert.NotNull(changeTracker);
        }
    }
}