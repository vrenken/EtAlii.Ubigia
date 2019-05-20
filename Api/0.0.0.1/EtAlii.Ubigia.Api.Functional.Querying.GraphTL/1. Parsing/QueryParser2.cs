//namespace EtAlii.Ubigia.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using Moppet.Lapa;
//
////    using System.Linq;
////    using Moppet.Lapa;
//
//    internal class QueryParser2 : IQueryParser
//    {
//        private const string Id = "Query";
//
////        private static readonly string[] _separators = new[] [ "\n", "\r\n" ]
//
//        private readonly INodeValidator _nodeValidator;
//        private readonly INodeFinder _nodeFinder;
//        private readonly LpsParser _parser;
//
//        public QueryParser2(
//            INodeValidator nodeValidator,
//            INodeFinder nodeFinder,
//            IConstantHelper constantHelper)
//        {
//            _nodeValidator = nodeValidator;
//            _nodeFinder = nodeFinder;
//
//            var textId = "Text";
//            var annotationId = "Annotation";
//
//            var newLine = Lp.One(c => c == '\n');
//            var whiteSpace = Lp.ZeroOrMore(c => c == ' ' || c == '\t');
//            var whiteSpaceOrNewLines = new LpsParser(whiteSpace | newLine).ZeroOrMore();
//            
//            var comment = Lp.Char('-') + Lp.Char('-') + Lp.ZeroOrMore(c => c != '\n');
//
//            var header = new LpsParser((whiteSpace + comment) | whiteSpace + newLine + whiteSpace).ZeroOrMore();
//            
//            var objectStart = Lp.One(c => c == '{'); 
//            var objectEnd = Lp.One(c => c == '}'); 
//
//            var annotation = whiteSpace + Lp.Char('@') + Lp.ZeroOrMore(c => c != '\n' && c != ',').Id(annotationId);
//
//            var quotedText = Lp.InBrackets(
//                    Lp.One(c => c == '\"'),
//                    Lp.OneOrMore(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).Id(textId),
//                    Lp.One(c => c == '\"')) |
//                Lp.InBrackets(
//                    Lp.One(c => c == '\''),
//                    Lp.OneOrMore(c => constantHelper.IsValidQuotedConstantCharacter(c, '\'')).Id(textId),
//                    Lp.One(c => c == '\''));
//            var unQuotedText = Lp.OneOrMore(c => constantHelper.IsValidQuotedConstantCharacter(c, '\"')).Id(textId);
//
//            var assignment = Lp.One(c => c == ':');
//
//            var key = new LpsParser("Key", true, quotedText | unQuotedText);
//            var value = new LpsParser("Value", true, quotedText | unQuotedText); 
//            
//            var keyOnly = key;
//            var keyValue = key + whiteSpace + assignment + whiteSpace + value;
//            var keyDefinition = key + whiteSpace + assignment + annotation;
//
//            var listItems = new LpsParser(keyOnly | keyValue | keyDefinition);
//            var delimiter = whiteSpaceOrNewLines + "," + whiteSpaceOrNewLines;
//            var list = Lp.List(listItems, delimiter, whiteSpaceOrNewLines.Maybe());
//            var objectDefinition = Lp.InBrackets(
//                whiteSpaceOrNewLines + objectStart,
//                list,
//                objectEnd + whiteSpaceOrNewLines); 
////            var keyValue = null;
//
//            
////            
////            var headerParsers = new[]
////            {
////                newLineParser.Optional,
////            }.Aggregate(new LpsAlternatives(), (current, parser) => current | parser);
////            
//            //_parser = new LpsParser(Id, true, headerParsers + newLineParser.Optional + annotationParser.Parser + newLineParser.Optional + objectParser.Parser + newLineParser.Optional); 
//            //_parser = new LpsParser(Id, true, commentParser.Parser + newLineParser.Required + objectParser.Parser + newLineParser.Optional); 
//            _parser = new LpsParser(Id, true, header);// + annotation); // + objectDefinition); 
//        }
//
//        public QueryParseResult Parse(string text)
//        {
//            text = text ?? string.Empty;
//
//            // Newlines and tabs are nasty. Correct them (newlines) or get rid of them (tabs). 
////            text = text.Replace("\r\n", "\n");
////            text = text.Replace("\t", " ");
//
//            var errors = Array.Empty<QueryParserError>();
//            Query query = null;
//
//            try
//            {
//                var node = _parser.Do(text);
//
//                _nodeValidator.EnsureSuccess(node, Id, false);
//
////                var objectDefinition = _nodeFinder.FindAll(node, _objectParser.Id)
////                    .Select(n => _objectParser.Parse(n))
////                    .Single();
//
//                query = new Query(null, null);//objectDefinition);
//            }
//            catch (Exception e)
//            {
//                errors = new[] { new QueryParserError(e, e.Message, 0, 0) };
//            }
//
//            return new QueryParseResult(string.Join(Environment.NewLine, text), query, errors);
//        }
//
//        public QueryParseResult Parse(string[] text)
//        {
//            return Parse(string.Join("\n", text));
//        }
//    }
//}
