// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
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
            _logger
                .ForContext("Script", script, true)
                .Information("Planning script");
            var start = Environment.TickCount;

            var result = _decoree.Plan(script);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger
                .ForContext("ScriptPlanResult", result, true)
                .Information("Planned script (Duration: {Duration}ms)", duration);

            return result;
        }
    }
}
