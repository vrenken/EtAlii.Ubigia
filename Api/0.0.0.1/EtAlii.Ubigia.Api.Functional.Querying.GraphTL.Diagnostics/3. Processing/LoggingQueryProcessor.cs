namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Querying 
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Logging;

    [DebuggerStepThrough]
    internal class LoggingQueryProcessor : IQueryProcessor
    {
        private readonly IQueryProcessor _processor;
        private readonly ILogger _logger;

        public LoggingQueryProcessor(
            IQueryProcessor processor,
            ILogger logger)
        {
            _processor = processor;
            _logger = logger;
        }

        public async Task<QueryProcessingResult> Process(Query query)
        {
            var message = "Processing query (async)";
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = await _processor.Process(query);

            message =
                $"Processed query (Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return result;
        }
    }
}
