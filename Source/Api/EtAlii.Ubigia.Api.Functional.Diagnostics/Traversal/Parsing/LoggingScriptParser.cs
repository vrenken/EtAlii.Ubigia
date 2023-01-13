// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using Serilog;
using EtAlii.xTechnology.Threading;
using EtAlii.xTechnology.Diagnostics;

internal class LoggingScriptParser : IScriptParser
{
    private readonly IScriptParser _parser;
    private readonly IContextCorrelator _contextCorrelator;
    private readonly ILogger _logger = Log.ForContext<IScriptParser>();

    public LoggingScriptParser(IScriptParser parser, IContextCorrelator contextCorrelator)
    {
        _parser = parser;
        _contextCorrelator = contextCorrelator;
    }

    public ScriptParseResult Parse(string text, ExecutionScope scope)
    {
        // We want to be able to track method calls throughout the whole application stack.
        // Including across network and process boundaries.
        // For this we create a unique correlationId and pass it through all involved systems.
        using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            var line = lines.Length == 1 ? lines[0] : $"{lines[0]}...";
            _logger.Debug("Parsing text (Text: {Line})", line);
            var start = Environment.TickCount;

            var scriptParseResult = _parser.Parse(text, scope);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Parsed text (Text: \"{Line}\" Duration: {Duration}ms)", line, duration);

            return scriptParseResult;
        }
    }

    public ScriptParseResult Parse(string[] text, ExecutionScope scope)
    {
        // We want to be able to track method calls throughout the whole application stack.
        // Including across network and process boundaries.
        // For this we create a unique correlationId and pass it through all involved systems.
        using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
        {
            var line = text.Length == 1 ? text[0] : $"{text[0]}...";
            _logger.Debug("Parsing text (Text: {Line})", line);
            var start = Environment.TickCount;

            var scriptParseResult = _parser.Parse(text, scope);

            var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
            _logger.Debug("Parsed text (Text: \"{Line}\" Duration: {Duration}ms)", line, duration);

            return scriptParseResult;
        }
    }
}
