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

        // Warning S107 Constructor has 21 parameters, which is greater than the 7 authorized.
        #pragma warning disable S107
        public PathSubjectPartsParser(
            ITraversingWildcardPathSubjectPartParser traversingWildcardPathSubjectPartParser,
            IWildcardPathSubjectPartParser wildcardPathSubjectPartParser,
            IConditionalPathSubjectPartParser conditionalPathSubjectPartParser,
            IConstantPathSubjectPartParser constantPathSubjectPartParser,
            IVariablePathSubjectPartParser variablePathSubjectPartParser,
            IIdentifierPathSubjectPartParser identifierPathSubjectPartParser,
            
            IAllParentsPathSubjectPartParser allParentsPathSubjectPartParser,
            IParentPathSubjectPartParser parentPathSubjectPartParser,
            IAllChildrenPathSubjectPartParser allChildrenPathSubjectPartParser,
            IChildrenPathSubjectPartParser childrenPathSubjectPartParser,
            
            IAllDowndatesPathSubjectPartParser allDowndatesPathSubjectPartParser,
            IDowndatePathSubjectPartParser downdatePathSubjectPartParser,
            IAllUpdatesPathSubjectPartParser allUpdatesPathSubjectPartParser,
            IUpdatesPathSubjectPartParser updatesPathSubjectPartParser,
            
            IAllPreviousPathSubjectPartParser allPreviousPathSubjectPartParser,
            IPreviousPathSubjectPartParser previousPathSubjectPartParser,
            IAllNextPathSubjectPartParser allNextPathSubjectPartParser,
            INextPathSubjectPartParser nextPathSubjectPartParser,
            
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
                
                allParentsPathSubjectPartParser,
                parentPathSubjectPartParser,
                allChildrenPathSubjectPartParser,
                childrenPathSubjectPartParser,

                allDowndatesPathSubjectPartParser,
                downdatePathSubjectPartParser,
                allUpdatesPathSubjectPartParser,
                updatesPathSubjectPartParser,

                allPreviousPathSubjectPartParser,
                previousPathSubjectPartParser, 
                allNextPathSubjectPartParser,
                nextPathSubjectPartParser,

                typedPathSubjectPartParser,
                regexPathSubjectPartParser
            };
            _nodeValidator = nodeValidator;
            var lpsParsers = _parsers.Aggregate(new LpsAlternatives(), (current, parser) => current | parser.Parser);
            Parser = new LpsParser(Id, true, lpsParsers);
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
