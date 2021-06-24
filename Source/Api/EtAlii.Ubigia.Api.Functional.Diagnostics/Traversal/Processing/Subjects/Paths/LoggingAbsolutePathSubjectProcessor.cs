// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    internal class LoggingAbsolutePathSubjectProcessor : IAbsolutePathSubjectProcessor
    {
        private readonly IAbsolutePathSubjectProcessor _decoree;
        private readonly ILogger _logger = Log.ForContext<IAbsolutePathSubjectProcessor>();

        public LoggingAbsolutePathSubjectProcessor(IAbsolutePathSubjectProcessor decoree)
        {
            _decoree = decoree;
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            _logger.Information("Processing absolute path {$PathSubject}", subject);
            var start = Environment.TickCount;

            await _decoree.Process(subject, scope, output).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Information("Processed absolute path (Duration: {Duration}ms)", duration);
        }
    }
}
