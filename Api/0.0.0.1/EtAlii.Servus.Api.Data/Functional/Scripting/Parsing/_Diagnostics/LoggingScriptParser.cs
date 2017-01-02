namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Logging;

    public class LoggingScriptParser : IScriptParser
    {
        private readonly IScriptParser _parser;
        private readonly ILogger _logger;

        public LoggingScriptParser(IScriptParser parser, ILogger logger)
        {
            _parser = parser;
            _logger = logger;
        }

        public Script Parse(string text)
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            var line = lines.Length == 1 ? lines[0] : String.Format("{0}...", lines[0]);
            var message = String.Format("Parsing text (Text: {0})", line);
            _logger.Info(message);
            var start = Environment.TickCount;

            var script = _parser.Parse(text);

            message = String.Format("Parsed text (Text: \"{0}\" Duration: {1}ms)", line, Environment.TickCount - start);
            _logger.Info(message);

            return script;
        }
    }
}
