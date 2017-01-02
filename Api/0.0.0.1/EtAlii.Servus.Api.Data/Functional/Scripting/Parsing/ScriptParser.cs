namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal class ScriptParser : IScriptParser
    {
        private static readonly string[] _separators = new string[] { "\n", "\r\n" };

        private readonly SequenceParser _sequenceParser;

        public ScriptParser(SequenceParser sequenceParser)
        {
            _sequenceParser = sequenceParser;
        }

        public Script Parse(string text)
        {
            text = text ?? String.Empty;

            var lines = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

            var expressions = lines.Select(line => _sequenceParser.Parse(line));

            return new Script(expressions);
        }
    }
}
