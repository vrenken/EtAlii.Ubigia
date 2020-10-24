namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Diagnostics;
    using EtAlii.xTechnology.Diagnostics;
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
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (ContextCorrelator.BeginCorrelationScope("CorrelationId", Guid.NewGuid(), false))
            {
                var message = "Processing script (async)";
                _logger.Information(message);
                var start = Environment.TickCount;

                var result = _processor.Process(script);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Processed script (Duration: {Duration}ms)", duration);

                return result;
            }
        }
    }
}
