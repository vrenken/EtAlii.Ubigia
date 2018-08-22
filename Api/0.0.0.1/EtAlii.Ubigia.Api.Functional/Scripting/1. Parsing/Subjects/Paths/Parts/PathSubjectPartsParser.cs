namespace EtAlii.Ubigia.Api.Functional
{
    using System.Linq;
    using Moppet.Lapa;

    internal class PathSubjectPartsParser : IPathSubjectPartsParser
    {
        public string Id { get; } = "PathSubjectsPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartParser[] _parsers;

        public PathSubjectPartsParser(
            ITraversingWildcardPathSubjectPartParser traversingWildcardPathSubjectPartParser,
            IWildcardPathSubjectPartParser wildcardPathSubjectPartParser,
            IConditionalPathSubjectPartParser conditionalPathSubjectPartParser,
            IConstantPathSubjectPartParser constantPathSubjectPartParser,
            IVariablePathSubjectPartParser variablePathSubjectPartParser,
            IIdentifierPathSubjectPartParser identifierPathSubjectPartParser,
            IParentPathSubjectPartParser parentPathSubjectPartParser,
            IChildPathSubjectPartParser childPathSubjectPartParser,
            IDowndatePathSubjectPartParser downdatePathSubjectPartParser,
            IUpdatesPathSubjectPartParser updatesPathSubjectPartParser,
            ITypedPathSubjectPartParser typedPathSubjectPartParser,
            IRegexPathSubjectPartParser regexPathSubjectPartParser,
            INodeValidator nodeValidator)
        {
            _parsers = new IPathSubjectPartParser[]
            {
                traversingWildcardPathSubjectPartParser,
                wildcardPathSubjectPartParser,
                conditionalPathSubjectPartParser, 
                constantPathSubjectPartParser,
                variablePathSubjectPartParser,
                identifierPathSubjectPartParser,
                
                parentPathSubjectPartParser,
                childPathSubjectPartParser,
                
                downdatePathSubjectPartParser,
                updatesPathSubjectPartParser,
                
                typedPathSubjectPartParser,
                regexPathSubjectPartParser
            };
            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);//.Debug("PathSubjectParts", true);
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
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
