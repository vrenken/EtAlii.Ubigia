namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class LapaScriptParser : IScriptParser
    {
        private const string Id = "Script";

        private readonly ISequenceParser _sequenceParser;
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;
        private readonly ITraversalValidator _traversalValidator;

        public LapaScriptParser(
            ISequenceParser sequenceParser,
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            ITraversalValidator traversalValidator)
        {
            _sequenceParser = sequenceParser;
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _traversalValidator = traversalValidator;

            var firstParser = newLineParser.Optional + sequenceParser.Parser;
            var nextParser = newLineParser.Required + sequenceParser.Parser;

            _parser = new LpsParser(Id, true, firstParser.NextZeroOrMore(nextParser) + newLineParser.Optional);
        }

        public ScriptParseResult Parse(string text)
        {
            text ??= string.Empty;

            // Newlines and tabs are nasty. Correct them (newlines) or get rid of them (tabs).
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\t", " ");

            var errors = Array.Empty<ScriptParserError>();
            Script script = null;

            try
            {
                var node = _parser.Do(text);

                _nodeValidator.EnsureSuccess(node, Id, false);

                var sequences = _nodeFinder.FindAll(node, _sequenceParser.Id)
                    .Select(n => n.Match.ToString())
                    .Select(t => _sequenceParser.Parse(t))
                    .ToArray();

                script = new Script(sequences);

                _traversalValidator.Validate(script);
            }
            catch (Exception e)
            {
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
                script = null;
            }

            return new ScriptParseResult(string.Join(Environment.NewLine, text), script, errors);
        }

        public ScriptParseResult Parse(string[] text) =>  Parse(string.Join("\n", text));
    }
}
