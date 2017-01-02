namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Logging;

    public class LoggingScriptProcessor : IScriptProcessor
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

        public void Process(Script script, ScriptScope scope, IDataConnection connection)
        {
            var message = "Processing script";
            _logger.Info(message);
            var start = Environment.TickCount;

            _processor.Process(script, scope, connection);

            message = String.Format("Processed script (Duration: {0}ms)", Environment.TickCount - start);
            _logger.Info(message);
        }
    }
}
