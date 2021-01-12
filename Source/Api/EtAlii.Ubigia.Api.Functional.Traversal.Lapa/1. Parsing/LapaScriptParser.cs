namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

    internal class LapaScriptParser : IScriptParser
    {
        private const string _id = "Script";

//        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]

        private readonly ISequenceParser _sequenceParser;
        private readonly INodeValidator _nodeValidator;
        private readonly INonRootedPathSubjectParser _nonRootedPathSubjectParser;
        private readonly IRootedPathSubjectParser _rootedPathSubjectParser;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;
        private readonly IScriptValidator _scriptValidator;
        private readonly LpsParser _nonRootedParser;
        private readonly LpsParser _rootedParser;

        public LapaScriptParser(
            ISequenceParser sequenceParser,
            INodeValidator nodeValidator,
            INonRootedPathSubjectParser nonRootedPathSubjectParser,
            IRootedPathSubjectParser rootedPathSubjectParser,
            INodeFinder nodeFinder,
            INewLineParser newLineParser,
            IScriptValidator scriptValidator)
        {
            _sequenceParser = sequenceParser;
            _nodeValidator = nodeValidator;
            _nonRootedPathSubjectParser = nonRootedPathSubjectParser;
            _rootedPathSubjectParser = rootedPathSubjectParser;
            _nodeFinder = nodeFinder;
            _scriptValidator = scriptValidator;

            var firstParser = newLineParser.Optional + sequenceParser.Parser;
            var nextParser = newLineParser.Required + sequenceParser.Parser;

            _parser = new LpsParser(_id, true, firstParser.NextZeroOrMore(nextParser) + newLineParser.Optional);

            _nonRootedParser = new LpsParser(_id, true, _nonRootedPathSubjectParser.Parser);
            _rootedParser = new LpsParser(_id, true, _rootedPathSubjectParser.Parser);
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

                _nodeValidator.EnsureSuccess(node, _id, false);

                //var sequences = node.Children
                //    .Where(n => n.Id == SequenceParser.Id)
                var sequences = _nodeFinder.FindAll(node, _sequenceParser.Id)
                    .Select(n => n.Match.ToString())
                    .Select(t => _sequenceParser.Parse(t))
                    .ToArray();

                script = new Script(sequences);

                _scriptValidator.Validate(script);
            }
            catch (Exception e)
            {
                errors = new[] { new ScriptParserError(e, e.Message, 0, 0) };
                script = null;
            }

            return new ScriptParseResult(string.Join(Environment.NewLine, text), script, errors);
        }

        public ScriptParseResult Parse(string[] text) =>  Parse(string.Join("\n", text));

        public Subject ParsePath(string text)
        {
            // TODO: This class should also be able to cope with rooted paths.
            var node = _nonRootedParser.Do(text);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            if (!_nonRootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse path (text: {text ?? "NULL"})");
            }
            var pathSubject = _nonRootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            _scriptValidator.Validate(pathSubject);

            return pathSubject;
        }

        public Subject ParseNonRootedPath(string text)
        {
            var node = _nonRootedParser.Do(text);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            if (!_nonRootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse non-rooted path (text: {text ?? "NULL"})");
            }
            var pathSubject = _nonRootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            _scriptValidator.Validate(pathSubject);

            return pathSubject;
        }

        public Subject ParseRootedPath(string text)
        {
            var node = _rootedParser.Do(text);
            _nodeValidator.EnsureSuccess(node, _id, false);
            var childNode = node.Children.Single();

            if (!_rootedPathSubjectParser.CanParse(childNode))
            {
                throw new ScriptParserException($"Unable to parse rooted path (text: {text ?? "NULL"})");
            }
            var pathSubject = _rootedPathSubjectParser.Parse(childNode);

            // There is a possibility that we receive a string constant that needs to be validated validation.
            _scriptValidator.Validate(pathSubject);

            return pathSubject;
        }
    }
}
