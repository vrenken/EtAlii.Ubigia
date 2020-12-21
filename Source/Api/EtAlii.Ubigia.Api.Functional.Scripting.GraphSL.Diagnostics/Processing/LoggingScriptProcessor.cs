﻿namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Diagnostics;
    using Serilog;
    using EtAlii.xTechnology.Threading;
    using EtAlii.xTechnology.Diagnostics;

    [DebuggerStepThrough]
    internal class LoggingScriptProcessor : IScriptProcessor
    {
        private readonly IScriptProcessor _processor;
        private readonly IContextCorrelator _contextCorrelator;
        private readonly ILogger _logger = Log.ForContext<IScriptProcessor>();

        public LoggingScriptProcessor(IScriptProcessor processor, IContextCorrelator contextCorrelator)
        {
            _processor = processor;
            _contextCorrelator = contextCorrelator;
        }

        public IObservable<SequenceProcessingResult> Process(Script script)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                _logger.Information("Processing script");
                var start = Environment.TickCount;

                var result = _processor.Process(script);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                _logger.Information("Processed script (Duration: {Duration}ms)", duration);

                return result;
            }
        }
    }
}
