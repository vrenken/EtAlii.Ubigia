namespace EtAlii.Servus.Api.Functional
{
    using System;
    using EtAlii.xTechnology.Logging;

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
            var line = lines.Length == 1 ? lines[0] : String.Format("{0}...", lines[0]);
            var message = String.Format("Parsing text (Text: {0})", line);
            _logger.Info(message);
            var start = Environment.TickCount;

            var scriptParseResult = _parser.Parse(text);

            message = String.Format("Parsed text (Text: \"{0}\" Duration: {1}ms)", line, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            _logger.Info(message);

            return scriptParseResult;
        }

        public ScriptParseResult Parse(string[] text)
        {
            var line = text.Length == 1 ? text[0] : String.Format("{0}...", text[0]);
            var message = String.Format("Parsing text (Text: {0})", line);
            _logger.Info(message);
            var start = Environment.TickCount;

            var scriptParseResult = _parser.Parse(text);

            message = String.Format("Parsed text (Text: \"{0}\" Duration: {1}ms)", line, TimeSpan.FromTicks(Environment.TickCount - start).TotalMilliseconds);
            _logger.Info(message);

            return scriptParseResult;
        }
    }
}
