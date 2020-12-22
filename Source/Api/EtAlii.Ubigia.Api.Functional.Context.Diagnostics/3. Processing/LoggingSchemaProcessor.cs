namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Serilog;

    [DebuggerStepThrough]
    internal class LoggingSchemaProcessor : ISchemaProcessor
    {
        private readonly ISchemaProcessor _processor;
        private readonly ILogger _logger = Log.ForContext<ISchemaProcessor>();

        public LoggingSchemaProcessor(ISchemaProcessor processor)
        {
            _processor = processor;
        }

        public async Task<SchemaProcessingResult> Process(Schema schema)
        {
            var message = "Processing query";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = await _processor.Process(schema).ConfigureAwait(false);

            message = $"Processed query (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Information(message);

            return result;
        }
    }
}
