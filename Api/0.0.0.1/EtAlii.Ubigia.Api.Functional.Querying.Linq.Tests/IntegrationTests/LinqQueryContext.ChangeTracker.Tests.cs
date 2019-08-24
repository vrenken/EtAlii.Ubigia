namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using Xunit;

    public class LinqQueryContextChangeTrackerTests
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