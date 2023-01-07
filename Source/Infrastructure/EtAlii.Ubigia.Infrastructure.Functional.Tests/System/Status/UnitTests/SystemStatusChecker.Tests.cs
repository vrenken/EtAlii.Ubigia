namespace EtAlii.Ubigia.Infrastructure.Functional.Tests
{
    using System;
    using Xunit;

    public class SystemStatusCheckerUnitTests
    {
        [Fact]
        public void SystemStatusChecker_Create()
        {
            // Arrange.

            // Act.
            var systemStatusChecker = new SystemStatusChecker();

            // Assert.
            Assert.NotNull(systemStatusChecker);
        }

        [Fact]
        public void SystemStatusChecker_Incorrect_Dependencies()
        {
            // Arrange.
            var systemStatusChecker = new SystemStatusChecker();

            // Act.
            var act = new Action(() => systemStatusChecker.DetermineIfSystemIsOperational(null, null));

            // Assert.
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
