namespace EtAlii.Ubigia.Api.Logical.Tests.UnitTests
{
    using Xunit;

    public class LogicalContextTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void LogicalContext_Create()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration();

            // Act.
            var context = new LogicalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void LogicalContext_Dispose()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration();

            // Act.
            using (var context = new LogicalContextFactory().Create(configuration))
            {
                // Assert.
                Assert.NotNull(context);
            }
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void LogicalContext_Create_Check_Components()
        {
            // Arrange.
            var configuration = new LogicalContextConfiguration();

            // Act.
            var context = new LogicalContextFactory().Create(configuration);

            // Assert.
            Assert.NotNull(context);
        }
    }
}
