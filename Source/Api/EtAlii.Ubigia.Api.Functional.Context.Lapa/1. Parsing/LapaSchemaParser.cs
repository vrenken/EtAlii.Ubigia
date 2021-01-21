namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Moppet.Lapa;

//    using System.Linq
//    using Moppet.Lapa

    internal class LapaSchemaParser : ISchemaParser
    {
        private readonly IStructureFragmentParser _structureFragmentParser;
        private const string _id = "Query";

//        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;

        public LapaSchemaParser(
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

            _parser = new LpsParser(_id, true, headerParsers + newLineParser.OptionalMultiple + _structureFragmentParser.Parser.Maybe() + newLineParser.OptionalMultiple);
        }

        public SchemaParseResult Parse(string text)
        {
            text ??= string.Empty;

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
