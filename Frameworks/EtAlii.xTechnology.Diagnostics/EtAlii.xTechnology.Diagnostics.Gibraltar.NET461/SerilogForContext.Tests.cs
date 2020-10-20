﻿namespace EtAlii.xTechnology.Diagnostics.Tests
{
    using System.Threading.Tasks;
    using Serilog;
    using Xunit;

    public class SerilogForContextTests
    {
        [Fact]
        public void ForContextRaiseInformation()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();

            // Act.
            logger.Information("Test to see if this log entry is shown");

            // Assert.
            Assert.True(hasConfiguration);
        }

        [Fact]
        public void ForContextRaiseInformationWithCorrelationId()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();

            // Act.
            using (ContextCorrelator.BeginCorrelationScope("TestCorrelationId", "42"))
            {
                logger.Information("Test to see if this log entry is shown together with a correlation id");
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

        [Fact]
        public async Task ForContextRaiseInformationWithCorrelationIdInAsyncMethod()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();

            // Act.
            using (ContextCorrelator.BeginCorrelationScope("TestCorrelationId", "42"))
            {
                await Task.Run(() => WriteLogAsync(logger));
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

        private void WriteLogAsync(ILogger logger)
        {
            logger.Information("Test to see if this log entry is shown together with a correlation id written in an async method");
        }
    }
}
