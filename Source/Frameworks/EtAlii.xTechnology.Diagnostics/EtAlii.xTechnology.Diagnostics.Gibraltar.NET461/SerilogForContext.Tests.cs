// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Diagnostics.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Serilog;
    using Xunit;
    using EtAlii.xTechnology.Threading;

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
            var contextCorrelator = new ContextCorrelator();

            // Act.
            using (contextCorrelator.BeginLoggingCorrelationScope("TestCorrelationId", "41"))
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
            var contextCorrelator = new ContextCorrelator();

            // Act.
            using (contextCorrelator.BeginLoggingCorrelationScope("TestCorrelationId", "42"))
            {
                logger.Information("Test to see if this log entry is shown followed by a correlation id written in an async method");
                await Task.Run(() => WriteLogAsync(logger)).ConfigureAwait(false);
            }

            // Assert.
            Assert.True(hasConfiguration);
        }

        [Fact]
        public async Task ForContextRaiseInformationWithCorrelationIdInAsyncMethodUsingDifferentClassAndLogger()
        {
            // Arrange.
            var hasConfiguration = DiagnosticsConfiguration.Default != null;
            var logger = Log.ForContext<SerilogForContextTests>();
            var testClass = new TestClass();
            var contextCorrelator = new ContextCorrelator();

            // Act.
            using (contextCorrelator.BeginLoggingCorrelationScope("TestCorrelationId", "42"))
            {
                logger.Information("Test to see if this log entry is shown followed by a correlation id written in an async method");
                await Task.Run(() => testClass.WriteLogAsync()).ConfigureAwait(false);
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
            var contextCorrelator = new ContextCorrelator();

            // Act.
            using (contextCorrelator.BeginLoggingCorrelationScope("TestCorrelationId", "43"))
            {
                logger.Information("Test to see if this log entry is shown followed by a correlation id written in an async method");
                await Task.Run(WriteLogAsyncWithNewLogger).ConfigureAwait(false);
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
            var contextCorrelator = new ContextCorrelator();

            // Act.
            using (contextCorrelator.BeginLoggingCorrelationScope("TestCorrelationId", "44"))
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

