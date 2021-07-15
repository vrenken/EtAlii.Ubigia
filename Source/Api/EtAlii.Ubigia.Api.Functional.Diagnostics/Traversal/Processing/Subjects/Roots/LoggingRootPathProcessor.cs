// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    internal class LoggingRootPathProcessor : IRootPathProcessor
    {
        private readonly IRootPathProcessor _decoree;
        private readonly ILogger _logger = Log.ForContext<IRootPathProcessor>();

        public LoggingRootPathProcessor(IRootPathProcessor decoree)
        {
            _decoree = decoree;
        }

        public async Task Process(string root, PathSubjectPart[] path, ExecutionScope scope, IObserver<object> output, IScriptScope scriptScope)
        {
            _logger.Debug("Processing root {RootSubject}", root);
            var start = Environment.TickCount;

            await _decoree.Process(root, path, scope, output, scriptScope).ConfigureAwait(false);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Processed root (Duration: {Duration}ms)", duration);
        }
    }
}
