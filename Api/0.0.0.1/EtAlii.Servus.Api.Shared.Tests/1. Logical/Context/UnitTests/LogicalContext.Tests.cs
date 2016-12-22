namespace EtAlii.Servus.Api.Logical.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Xunit;

    
    public class LogicalContext_Tests
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
            }

            // Assert.
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
