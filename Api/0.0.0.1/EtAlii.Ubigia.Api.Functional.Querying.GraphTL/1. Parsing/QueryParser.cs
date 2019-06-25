namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using Moppet.Lapa;

//    using System.Linq;
//    using Moppet.Lapa;

    internal class QueryParser : IQueryParser
    {
        private readonly ICommentParser _commentParser;
        private readonly IStructureQueryParser _structureQueryParser;
        private readonly IStructureMutationParser _structureMutationParser;
        private const string Id = "Query";

//        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]

        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;

        public QueryParser(
            ICommentParser commentParser,
            IAnnotationParser annotationParser,
            IStructureQueryParser structureQueryParser,
            IStructureMutationParser structureMutationParser,
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser)
        {
            _commentParser = commentParser;
            _structureQueryParser = structureQueryParser;
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;
            _structureMutationParser = structureMutationParser;

            var structureParsers = new LpsParser(new[]
            {
                _structureQueryParser.Parser,
                _structureMutationParser.Parser
            }.Aggregate(new LpsAlternatives(), (current, parser) => current | parser)).Maybe();
            
            var headerParsers = (newLineParser.OptionalMultiple + commentParser.Parser).ZeroOrMore();

            //var headerParsers = (commentParsers).Maybe();
            _parser = new LpsParser(Id, true, headerParsers + newLineParser.OptionalMultiple + structureParsers + newLineParser.OptionalMultiple); 
            ////_parser = new LpsParser(Id, true, commentParser.Parser + newLineParser.Required + structureQueryParser.Parser + newLineParser.Optional); 
            //_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + commentParser.Parser + newLineParser.OptionalMultiple + _structureQueryParser.Parser); 
            ////_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + commentParser.Parser + newLineParser.OptionalMultiple + annotationParser.Parser); 
            //_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + _structureQueryParser.Parser + newLineParser.OptionalMultiple); 
            //_parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + _structureQueryParser.Parser); 
        }

        public QueryParseResult Parse(string text)
        {
            text = text ?? string.Empty;

            // Newlines and tabs are nasty. Correct them (newlines) or get rid of them (tabs). 
//            text = text.Replace("\r\n", "\n");
//            text = text.Replace("\t", " ");

            var errors = Array.Empty<QueryParserError>();
            Query query = null;

            try
            {
                var node = _parser.Do(text);

                _nodeValidator.EnsureSuccess(node, Id, false);

                if (_nodeFinder.FindFirst(node, _structureQueryParser.Id) is LpNode structureQueryMatch)
                {
                    var structureQuery = _structureQueryParser.Parse(structureQueryMatch); 
                    query = new Query(structureQuery);
                }
                else if (_nodeFinder.FindFirst(node, _structureMutationParser.Id) is LpNode structureMutationMatch)
                {
                    var structureMutation = _structureMutationParser.Parse(structureMutationMatch); 
                    query = new Query(structureMutation);
                }
            }
            catch (Exception e)
            {
                errors = new[] { new QueryParserError(e, e.Message, 0, 0) };
            }

            return new QueryParseResult(string.Join(Environment.NewLine, text), query, errors);
        }

        public QueryParseResult Parse(string[] text)
        {
            return Parse(string.Join("\n", text));
        }
    }
}
