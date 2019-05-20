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
        private readonly IObjectParser _objectParser;
        private const string Id = "Query";

//        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]

        private readonly IAnnotationParser _annotationParser;
        private readonly INodeValidator _nodeValidator;
        private readonly INodeFinder _nodeFinder;
        private readonly LpsParser _parser;

        public QueryParser(
            ICommentParser commentParser,
            IAnnotationParser annotationParser,
            IObjectParser objectParser,
            INodeValidator nodeValidator,
            INodeFinder nodeFinder,
            INewLineParser newLineParser)
        {
            _commentParser = commentParser;
            _objectParser = objectParser;
            _annotationParser = annotationParser;
            _nodeValidator = nodeValidator;
            _nodeFinder = nodeFinder;

            var headerParsers = new[]
            {
                newLineParser.Optional,
                commentParser.Parser,
            }.Aggregate(new LpsAlternatives(), (current, parser) => current | parser);
            
            //_parser = new LpsParser(Id, true, headerParsers + newLineParser.Optional + annotationParser.Parser + newLineParser.Optional + objectParser.Parser + newLineParser.Optional); 
            //_parser = new LpsParser(Id, true, commentParser.Parser + newLineParser.Required + objectParser.Parser + newLineParser.Optional); 
            _parser = new LpsParser(Id, true, newLineParser.OptionalMultiple + commentParser.Parser + newLineParser.OptionalMultiple + _annotationParser.Parser); 
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

                var objectDefinitionMatch = _nodeFinder.FindFirst(node, _objectParser.Id);
                var objectDefinition = _objectParser.Parse(objectDefinitionMatch); 

                query = new Query(objectDefinition);
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
