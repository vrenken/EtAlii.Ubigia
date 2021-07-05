// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Linq;
    using Moppet.Lapa;

    internal class PathSubjectPartsParser : IPathSubjectPartsParser
    {
        public string Id => "PathSubjectsPart";

        public LpsParser Parser { get; }

        private readonly INodeValidator _nodeValidator;
        private readonly IPathSubjectPartParser[] _parsers;

        // SONARQUBE_DependencyInjectionSometimesRequiresMoreThan7Parameters:
        // After a (very) long period of considering all options I am convinced that we won't be able to break down all DI patterns so that they fit within the 7 limit
        // specified by SonarQube. The current setup here is already some kind of facade that hides away many specific parsing variations. Therefore refactoring to facades won't work.
        // Therefore this pragma warning disable of S107.
#pragma warning disable S107
        public PathSubjectPartsParser(
            ITraversingWildcardPathSubjectPartParser traversingWildcardPathSubjectPartParser,
            IWildcardPathSubjectPartParser wildcardPathSubjectPartParser,
            ITaggedPathSubjectPartParser taggedPathSubjectPartParser,

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
#pragma warning restore S107
        {
            _parsers = new IPathSubjectPartParser[]
            {
                traversingWildcardPathSubjectPartParser,
                wildcardPathSubjectPartParser,
                taggedPathSubjectPartParser,
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
            Parser = new LpsParser(Id, true, lpsParsers);//.Debug("PathSubjectParts", true)
        }

        public PathSubjectPart Parse(LpNode node)
        {
            _nodeValidator.EnsureSuccess(node, Id);
            var childNode = node.Children.Single();
            var parser = _parsers.Single(p => p.CanParse(childNode));
            var result = parser.Parse(childNode);
            return result;
        }
    }
}
