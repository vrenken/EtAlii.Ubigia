namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Tests;
    using Xunit;

    [CorrelateUnitTests]
    public class SystemStatusCheckerIntegrationTests
    {
        [Fact]
        public async Task SystemStatusChecker_DetermineIfSystemIsOperational()
        {
            // Arrange.
            var testContext = new FunctionalInfrastructureUnitTestContext();
            await testContext
                .InitializeAsync()
                .ConfigureAwait(false);

            var systemStatusChecker = new SystemStatusChecker();
            bool result;

            // Act.
            try
            {
                result = systemStatusChecker.DetermineIfSystemIsOperational(testContext.Functional, testContext.Configuration);
            }
            finally
            {
                await testContext
                    .DisposeAsync()
                    .ConfigureAwait(false);
            }

            // Assert.
            Assert.NotNull(systemStatusChecker);
            Assert.True(result);
        }
    }
}
