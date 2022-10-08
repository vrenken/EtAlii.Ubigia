namespace EtAlii.Ubigia.Infrastructure.Transport.Tests
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Hosting;
    using Xunit;

    public class SystemStatusCheckerTests
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
        public void SystemStatusChecker_DetermineIfSystemIsOperational()
        {
            // Arrange.
            var systemStatusChecker = new SystemStatusChecker();

            // Act.
            var act = new Action(() => systemStatusChecker.DetermineIfSystemIsOperational(new List<IService>(), null));

            // Assert.
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
