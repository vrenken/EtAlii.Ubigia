namespace EtAlii.Ubigia.Api.Functional.Diagnostics 
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;

    [DebuggerStepThrough]
    internal class LoggingSchemaProcessor : ISchemaProcessor
    {
        private readonly ISchemaProcessor _processor;
        private readonly ILogger _logger;

        public LoggingSchemaProcessor(
            ISchemaProcessor processor,
            ILogger logger)
        {
            _processor = processor;
            _logger = logger;
        }

        public async Task<SchemaProcessingResult> Process(Schema schema)
        {
            var message = "Processing query (async)";
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = await _processor.Process(schema);

            message =
                $"Processed query (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return result;
        }
    }
}
