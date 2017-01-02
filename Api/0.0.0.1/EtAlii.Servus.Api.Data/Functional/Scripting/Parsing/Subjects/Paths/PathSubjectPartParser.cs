namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using EtAlii.Servus.Api.Data.Model;

    internal class PathSubjectPartParser
    {
        public const string Id = "PathSubjectPart"; 
        public readonly LpsParser Parser;
        private readonly IParserHelper _parserHelper;
        private readonly IEnumerable<IPathSubjectPartParser> _parsers;

        public PathSubjectPartParser(IParserHelper parserHelper, IEnumerable<IPathSubjectPartParser> parsers)
        {
            _parserHelper = parserHelper;
            _parsers = parsers;
            var lpsParsers = new LpsAlternatives();
            foreach (var parser in _parsers)
            {
                lpsParsers |= parser.Parser;
            }
            Parser = new LpsParser(Id, true, lpsParsers);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _parserHelper.EnsureSuccess2(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }

        public void Validate(PathSubjectPart before, PathSubjectPart part, int partIndex, PathSubjectPart after)
        {
            var parser = _parsers.Single(p => p.CanValidate(part));
            parser.Validate(before, part, partIndex, after);
        }
    }
}
