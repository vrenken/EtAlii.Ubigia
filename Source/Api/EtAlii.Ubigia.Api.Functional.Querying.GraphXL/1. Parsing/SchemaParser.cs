namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

//    using System.Linq
//    using Moppet.Lapa

    internal class SchemaParser : ISchemaParser
    {
        private readonly IStructureFragmentParser _structureFragmentParser;
        private const string _id = "Query";

//        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;

        public SchemaParser(
            ICommentParser commentParser,
            IStructureFragmentParser structureFragmentParser,
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser)
        {
            _structureFragmentParser = structureFragmentParser;
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var headerParsers = (newLineParser.OptionalMultiple + commentParser.Parser).ZeroOrMore();

            //var headerParsers = (commentParsers).Maybe()
            _parser = new LpsParser(_id, true, headerParsers + newLineParser.OptionalMultiple + _structureFragmentParser.Parser.Maybe() + newLineParser.OptionalMultiple);
            ////_parser = new LpsParser(Id, true, commentParser.Parser + newLineParser.Required + structureQueryParser.Parser + newLineParser.Optional)
            //_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + commentParser.Parser + newLineParser.OptionalMultiple + _structureQueryParser.Parser)
            ////_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + commentParser.Parser + newLineParser.OptionalMultiple + annotationParser.Parser)
            //_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + _structureQueryParser.Parser + newLineParser.OptionalMultiple)
            //_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + _structureQueryParser.Parser)
        }

        public SchemaParseResult Parse(string text)
        {
            text ??= string.Empty;

            // Newlines and tabs are nasty. Correct them (newlines) or get rid of them (tabs).
//            text = text.Replace("\r\n", "\n")
//            text = text.Replace("\t", " ")

            var errors = Array.Empty<SchemaParserError>();
            Schema schema = null;

            try
            {
                var node = _parser.Do(text);

                _nodeValidator.EnsureSuccess(node, _id, false);

                if (_nodeFinder.FindFirst(node, _structureFragmentParser.Id) is { } structureFragmentMatch)
                {
                    var structureFragment = _structureFragmentParser.Parse(structureFragmentMatch);
                    schema = new Schema(structureFragment);
                }
            }
            catch (Exception e)
            {
                errors = new[] { new SchemaParserError(e, e.Message, 0, 0) };
            }

            return new SchemaParseResult(string.Join(Environment.NewLine, text), schema, errors);
        }

        public SchemaParseResult Parse(string[] text)
        {
            return Parse(string.Join("\n", text));
        }
    }
}
