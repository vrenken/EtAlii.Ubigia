namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;


    public class DataContextChangeTrackerTests
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