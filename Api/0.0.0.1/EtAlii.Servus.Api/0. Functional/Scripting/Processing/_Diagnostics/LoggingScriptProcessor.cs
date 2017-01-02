namespace EtAlii.Servus.Api.Functional
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

        public void Process(Script script)
        {
            _processor.Process(script);
        }

        public void Process(Script script, IProgress<ScriptProcessingProgress> progress)
        {
            var message = "Processing script";
            _logger.Info(message);
            var start = Environment.TickCount;

            _processor.Process(script, progress);

            message = String.Format("Processed script (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);
        }
    }
}
