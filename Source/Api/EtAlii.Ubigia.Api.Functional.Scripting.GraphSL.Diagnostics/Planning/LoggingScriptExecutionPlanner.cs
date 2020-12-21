// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using Serilog;

    internal class LoggingScriptExecutionPlanner : IScriptExecutionPlanner
    {
        private readonly IScriptExecutionPlanner _decoree;
        private readonly ILogger _logger = Log.ForContext<IScriptExecutionPlanner>();

        public LoggingScriptExecutionPlanner(IScriptExecutionPlanner decoree)
        {
            _decoree = decoree;
        }

        public ISequenceExecutionPlan[] Plan(Script script)
        {
            _logger.Information("Planning script", script);
            var start = Environment.TickCount;

            var result = _decoree.Plan(script);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Planned script (Duration: {Duration}ms)", duration, result);

            return result;
        }
    }
}
