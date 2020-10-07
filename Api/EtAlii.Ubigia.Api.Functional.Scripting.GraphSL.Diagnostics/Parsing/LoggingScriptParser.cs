namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using EtAlii.xTechnology.Diagnostics;

    internal class LoggingScriptParser : IScriptParser
    {
        private readonly IScriptParser _parser;
        private readonly ILogger _logger;

        public LoggingScriptParser(IScriptParser parser, ILogger logger)
        {
            _parser = parser;
            _logger = logger;
        }

        public ScriptParseResult Parse(string text)
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            var line = lines.Length == 1 ? lines[0] : $"{lines[0]}...";
            var message = $"Parsing text (Text: {line})";
            _logger.Info(message);
            var start = Environment.TickCount;

            var scriptParseResult = _parser.Parse(text);

            message =
                $"Parsed text (Text: \"{line}\" Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return scriptParseResult;
        }

        public ScriptParseResult Parse(string[] text)
        {
            var line = text.Length == 1 ? text[0] : $"{text[0]}...";
            var message = $"Parsing text (Text: {line})";
            _logger.Info(message);
            var start = Environment.TickCount;

            var scriptParseResult = _parser.Parse(text);

            message =
                $"Parsed text (Text: \"{line}\" Duration: {TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds}ms)";
            _logger.Info(message);

            return scriptParseResult;
        }
    }
}
