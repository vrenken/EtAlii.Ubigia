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
            ((ISystemStatusChecker)systemStatusChecker).Initialize(testContext.Functional);
            bool result;

            // Act.
            try
            {
                result = await systemStatusChecker
                    .DetermineIfSystemIsOperational()
                    .ConfigureAwait(false);
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
