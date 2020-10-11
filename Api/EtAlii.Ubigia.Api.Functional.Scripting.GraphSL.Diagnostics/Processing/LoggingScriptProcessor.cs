namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Diagnostics;
    using Serilog;

    [DebuggerStepThrough]
    internal class LoggingScriptProcessor : IScriptProcessor
    {
        private readonly IScriptProcessor _processor;
        private readonly ILogger _logger = Log.ForContext<IScriptProcessor>();

        public LoggingScriptProcessor(IScriptProcessor processor)
        {
            _processor = processor;
        }

        public IObservable<SequenceProcessingResult> Process(Script script)
        {
            var message = "Processing script (async)";
            _logger.Information(message);
            var start = Environment.TickCount;

            var result = _processor.Process(script);

            _logger.Information("Processed script (Duration: {duration}ms)", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);

            return result;
        }
    }
}
