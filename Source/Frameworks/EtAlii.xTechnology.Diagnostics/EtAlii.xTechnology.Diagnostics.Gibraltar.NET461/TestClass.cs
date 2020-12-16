namespace EtAlii.xTechnology.Diagnostics.Tests
{
    using Serilog;

    public class TestClass
    {
        private readonly ILogger _logger = Log.ForContext<TestClass>();

        public void WriteLogAsync()
        {
            _logger.Information("Test to see if this log entry is shown together with a correlation id written in an async method");
        }
    }
}

