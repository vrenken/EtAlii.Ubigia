namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using EtAlii.xTechnology.Diagnostics;
    using Serilog;

    internal class LoggingScriptParser : IScriptParser
    {
        private readonly IScriptParser _parser;
        private readonly ILogger _logger = Log.ForContext<IScriptParser>();

        public LoggingScriptParser(IScriptParser parser)
        {
            _parser = parser;
        }

        public ScriptParseResult Parse(string text)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (ContextCorrelator.BeginCorrelationScope("CorrelationId", Guid.NewGuid(), false))
            {
                var lines = text.Replace("\r\n", "\n").Split('\n');
                var line = lines.Length == 1 ? lines[0] : $"{lines[0]}...";
                var message = "Parsing text (Text: {line})";
                _logger.Information(message, line);
                var start = Environment.TickCount;

                var scriptParseResult = _parser.Parse(text);

                var duration = TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds;
                message = "Parsed text (Text: \"{line}\" Duration: {duration}ms)";
                _logger.Information(message, line, duration);
                
                return scriptParseResult;
            }
        }

        public ScriptParseResult Parse(string[] text)
        {
            // We want to be able to track method calls throughout the whole application stack.
            // Including across network and process boundaries.
            // For this we create a unique correlationId and pass it through all involved systems.
            using (ContextCorrelator.BeginCorrelationScope("CorrelationId", Guid.NewGuid(), false))
            {
                var line = text.Length == 1 ? text[0] : $"{text[0]}...";
                var message = $"Parsing text (Text: {line})";
                _logger.Information(message);
                var start = Environment.TickCount;

                var scriptParseResult = _parser.Parse(text);

                message = $"Parsed text (Text: \"{line}\" Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
                _logger.Information(message);

                return scriptParseResult;
            }
        }
    }
}
