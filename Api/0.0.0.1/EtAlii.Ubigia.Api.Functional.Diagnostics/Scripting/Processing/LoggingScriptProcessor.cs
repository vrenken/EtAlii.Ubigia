namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting.Processing
{
    using System;
    using System.Diagnostics;
    using EtAlii.xTechnology.Logging;

    [DebuggerStepThrough]
    internal class LoggingScriptProcessor : IScriptProcessor
    {
        private readonly IScriptProcessor _processor;
        private readonly ILogger _logger;

        public LoggingScriptProcessor(
            IScriptProcessor processor,
            ILogger logger)
        {
            _processor = processor;
            _logger = logger;
        }

        public IObservable<SequenceProcessingResult> Process(Script script)
        {
            var message = "Processing script (async)";
            _logger.Info(message);
            var start = Environment.TickCount;

            var result = _processor.Process(script);

            message = String.Format("Processed script (Duration: {0}ms)", TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            _logger.Info(message);

            return result;
        }
    }
}
