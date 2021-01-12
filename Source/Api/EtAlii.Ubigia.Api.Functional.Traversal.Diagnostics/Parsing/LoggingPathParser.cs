﻿namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using Serilog;
    using EtAlii.xTechnology.Threading;
    using EtAlii.xTechnology.Diagnostics;

    internal class LoggingPathParser : IPathParser
    {
        private readonly IPathParser _parser;
        private readonly IContextCorrelator _contextCorrelator;
        private readonly ILogger _logger = Log.ForContext<IScriptParser>();

        public LoggingPathParser(IPathParser parser, IContextCorrelator contextCorrelator)
        {
            _parser = parser;
            _contextCorrelator = contextCorrelator;
        }

        public Subject ParsePath(string text)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                var lines = text.Replace("\r\n", "\n").Split('\n');
                var line = lines.Length == 1 ? lines[0] : $"{lines[0]}...";
                var message = "Parsing subject text (Text: {Line})";
                _logger.Information(message, line);
                var start = Environment.TickCount;

                var pathSubject = _parser.ParsePath(text);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                message = "Parsed path text (Text: \"{Line}\" Duration: {Duration}ms)";
                _logger.Information(message, line, duration);

                return pathSubject;
            }
        }

        public Subject ParseNonRootedPath(string text)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                var lines = text.Replace("\r\n", "\n").Split('\n');
                var line = lines.Length == 1 ? lines[0] : $"{lines[0]}...";
                var message = "Parsing non-rooted path text (Text: {Line})";
                _logger.Information(message, line);
                var start = Environment.TickCount;

                var pathSubject = _parser.ParseNonRootedPath(text);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                message = "Parsed non-rooted path text (Text: \"{Line}\" Duration: {Duration}ms)";
                _logger.Information(message, line, duration);

                return pathSubject;
            }
        }

        public Subject ParseRootedPath(string text)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (_contextCorrelator.BeginLoggingCorrelationScope(Correlation.ScriptId, ShortGuid.New(), false))
            {
                var lines = text.Replace("\r\n", "\n").Split('\n');
                var line = lines.Length == 1 ? lines[0] : $"{lines[0]}...";
                var message = "Parsing rooted path text (Text: {Line})";
                _logger.Information(message, line);
                var start = Environment.TickCount;

                var pathSubject = _parser.ParseRootedPath(text);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                message = "Parsed rooted path text (Text: \"{Line}\" Duration: {Duration}ms)";
                _logger.Information(message, line, duration);

                return pathSubject;
            }
        }
    }
}
