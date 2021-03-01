namespace EtAlii.Ubigia.Api.Functional.Context.Diagnostics
{
    using System;
    using System.Collections.Generic;
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

        public async IAsyncEnumerable<Structure> Process(Schema schema)
        {
            _logger.Information("Processing query");
            var start = Environment.TickCount;

            var items = _processor
                .Process(schema)
                .ConfigureAwait(false);

            await foreach (var item in items)
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Processed query (Duration: {Duration}ms)", duration);
        }

        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(Schema schema)
        {
            _logger.Information("Processing query");
            var start = Environment.TickCount;

            var items = _processor
                .ProcessMultiple<TResult>(schema)
                .ConfigureAwait(false);

            await foreach (var item in items)
            {
                yield return item;
            }

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Processed query (Duration: {Duration}ms)", duration);
        }

        public async Task<TResult> ProcessSingle<TResult>(Schema schema)
        {
            _logger.Information("Processing query");
            var start = Environment.TickCount;

            var item = await _processor
                .ProcessSingle<TResult>(schema)
                .ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Processed query (Duration: {Duration}ms)", duration);

            return item;
        }
    }
}
