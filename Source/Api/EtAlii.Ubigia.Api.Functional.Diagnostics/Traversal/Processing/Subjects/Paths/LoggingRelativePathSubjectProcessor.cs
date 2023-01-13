// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using Serilog;
using System;
using System.Threading.Tasks;

internal class LoggingRelativePathSubjectProcessor : IRelativePathSubjectProcessor
{
    private readonly IRelativePathSubjectProcessor _decoree;
    private readonly ILogger _logger = Log.ForContext<IRelativePathSubjectProcessor>();

    public LoggingRelativePathSubjectProcessor(IRelativePathSubjectProcessor decoree)
    {
        _decoree = decoree;
    }

    public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
    {
        _logger.Debug("Processing relative path {$PathSubject}", subject);
        var start = Environment.TickCount;

        await _decoree.Process(subject, scope, output).ConfigureAwait(false);

        var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
        _logger.Debug("Processed relative path (Duration: {Duration}ms)", duration);
    }
}
