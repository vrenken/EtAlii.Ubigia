namespace EtAlii.xTechnology.Diagnostics.Tests
{
    using System.Threading;
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
            using (ContextCorrelator.BeginCorrelationScope("TestCorrelationId", "41"))
            {
                logger.Information("Test to see if this log entry is shown together with a correlation id");
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

        [Fact]
        public async Task ForContextRaiseInformationWithCorrelationIdInAsyncMethodUsingSameLogger()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();

            // Act.
            using (ContextCorrelator.BeginCorrelationScope("TestCorrelationId", "42"))
            {
                logger.Information("Test to see if this log entry is shown followed by a correlation id written in an async method");
                await Task.Run(() => WriteLogAsync(logger));
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

        private void WriteLogAsync(ILogger logger)
        {
            logger.Information("Test to see if this log entry is shown together with a correlation id written in an async method");
        }
        
        
        
        [Fact]
        public async Task ForContextRaiseInformationWithCorrelationIdInAsyncMethodUsingDifferentLoggers()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();

            // Act.
            using (ContextCorrelator.BeginCorrelationScope("TestCorrelationId", "43"))
            {
                logger.Information("Test to see if this log entry is shown followed by a correlation id written in an async method");
                await Task.Run(WriteLogAsyncWithNewLogger);
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

        private void WriteLogAsyncWithNewLogger()
        {
            
            var logger = Log.ForContext<SecondarySerilogForContextTests>();
            logger.Information("Test to see if this log entry is shown together with a correlation id written in an async method");
        }
        
        [Fact]
        public void ForContextRaiseInformationWithCorrelationIdInAsyncMethodUsingDifferentLoggersUsingThread()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();

            // Act.
            using (ContextCorrelator.BeginCorrelationScope("TestCorrelationId", "44"))
            {
                logger.Information("Test to see if this log entry is shown followed by a correlation id written in an async method");
                var thread = new Thread(WriteLogAsyncWithNewLogger);
                thread.Start();
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

    }
}

