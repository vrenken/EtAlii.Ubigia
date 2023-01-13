namespace EtAlii.Ubigia.Infrastructure.Functional.Tests;

using System;
using System.Threading.Tasks;
using Xunit;

public class SystemStatusCheckerUnitTests
{
    [Fact]
    public void SystemStatusChecker_Create()
    {
        // Arrange.

        // Act.
        var systemStatusChecker = new SystemStatusChecker();
        ((ISystemStatusChecker)systemStatusChecker).Initialize(null);

        // Assert.
        Assert.NotNull(systemStatusChecker);
    }

    [Fact]
    public void SystemStatusChecker_Incorrect_Dependencies()
    {
        // Arrange.
        var systemStatusChecker = new SystemStatusChecker();
        ((ISystemStatusChecker)systemStatusChecker).Initialize(null);

        // Act.
        var act = new Func<Task>(async () => await systemStatusChecker.DetermineIfSystemIsOperational().ConfigureAwait(false));

        // Assert.
        Assert.ThrowsAsync<NullReferenceException>(act);
    }
}
