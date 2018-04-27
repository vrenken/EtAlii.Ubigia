﻿namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class ScriptParser : IScriptParser
    {
        private const string _id = "Script";

        private static readonly string[] _separators = new[] { "\n", "\r\n" };

        private readonly ISequenceParser _sequenceParser;
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;

        public ScriptParser(
            ISequenceParser sequenceParser,
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser)
        {
            _sequenceParser = sequenceParser;
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var firstParser = newLineParser.Optional + sequenceParser.Parser;
            var nextParser = newLineParser.Required + sequenceParser.Parser;

            _parser = new LpsParser(_id, true, firstParser.NextZeroOrMore(nextParser) + newLineParser.Optional); 
        }

        public ScriptParseResult Parse(string text)
        {
            text = text ?? String.Empty;

            // Newlines and tabs are nasty. Correct them (newlines) or get rid of them (tabs). 
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\t", " ");

            var errors = new ScriptParserError[] { };
            Script script = null;

            try
            {
                var node = _parser.Do(text);

                _nodeValidator.EnsureSuccess(node, _id, false);

                //var sequences = node.Children
                //    .Where(n => n.Id == SequenceParser.Id)
                var sequences = _nodeFinder.FindAll(node, _sequenceParser.Id)
                    .Select(n => n.Match.ToString())
                    .Select(t => _sequenceParser.Parse(t))
                    .ToArray();

                script = new Script(sequences);
            }
            catch (Exception e)
            {
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
            }

            return new ScriptParseResult(String.Join(Environment.NewLine, text), script, errors);
        }

        public ScriptParseResult Parse(string[] text)
        {
            return Parse(String.Join("\n", text));
        }
    }
}
