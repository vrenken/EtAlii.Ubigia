namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class SystemStatusCheckerIntegrationTests
{
    [Fact]
    public async Task SystemStatusChecker_DetermineSystemStatus()
    {
        // Arrange.
        var testContext = new FunctionalUnitTestContext();
        await testContext
            .InitializeAsync()
            .ConfigureAwait(false);

        var systemStatusChecker = new SystemStatusChecker();
        ((ISystemStatusChecker)systemStatusChecker).Initialize(testContext.Functional);
        SystemStatus result;

        // Act.
        try
        {
            result = await systemStatusChecker
                .DetermineSystemStatus()
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
        Assert.Equal(SystemStatus.SystemIsOperational ,result);
    }
}
