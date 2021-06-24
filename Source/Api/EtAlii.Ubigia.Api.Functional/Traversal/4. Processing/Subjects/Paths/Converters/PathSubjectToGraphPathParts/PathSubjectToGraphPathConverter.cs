// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class PathSubjectToGraphPathConverter : IPathSubjectToGraphPathConverter
    {
        private readonly IConstantPathSubjectPartToGraphPathPartsConverter _constantPathSubjectPartToGraphPathPartsConverter;
        private readonly IIdentifierPathSubjectPartToGraphPathPartsConverter _identifierPathSubjectPartToGraphPathPartsConverter;
        private readonly IVariablePathSubjectPartToGraphPathPartsConverter _variablePathSubjectPartToGraphPathPartsConverter;
        private readonly IAllParentsPathSubjectPartToGraphPathPartsConverter _allParentsPathSubjectPartToGraphPathPartsConverter;
        private readonly IParentPathSubjectPartToGraphPathPartsConverter _parentPathSubjectPartToGraphPathPartsConverter;
        private readonly IAllChildrenPathSubjectPartToGraphPathPartsConverter _allChildrenPathSubjectPartToGraphPathPartsConverter;
        private readonly IChildrenPathSubjectPartToGraphPathPartsConverter _childrenPathSubjectPartToGraphPathPartsConverter;
        private readonly IAllDowndatesPathSubjectPartToGraphPathPartsConverter _allDowndatesPathSubjectPartToGraphPathPartsConverter;
        private readonly IDowndatePathSubjectPartToGraphPathPartsConverter _downdatePathSubjectPartToGraphPathPartsConverter;
        private readonly IAllUpdatesPathSubjectPartToGraphPathPartsConverter _allUpdatesPathSubjectPartToGraphPathPartsConverter;
        private readonly IUpdatesPathSubjectPartToGraphPathPartsConverter _updatesPathSubjectPartToGraphPathPartsConverter;
        private readonly IWildcardPathSubjectPartToGraphPathPartsConverter _wildcardPathSubjectPartToGraphPathPartsConverter;
        private readonly ITaggedPathSubjectPartToGraphPathPartsConverter _taggedPathSubjectPartToGraphPathPartsConverter;
        private readonly ITraversingWildcardPathSubjectPartToGraphPathPartsConverter _traversingWildcardPathSubjectPartToGraphPathPartsConverter;
        private readonly IConditionalPathSubjectPartToGraphPathPartsConverter _conditionalPathSubjectPartToGraphPathPartsConverter;

        public PathSubjectToGraphPathConverter(
            IConstantPathSubjectPartToGraphPathPartsConverter constantPathSubjectPartToGraphPathPartsConverter,
            IIdentifierPathSubjectPartToGraphPathPartsConverter identifierPathSubjectPartToGraphPathPartsConverter,
            IVariablePathSubjectPartToGraphPathPartsConverter variablePathSubjectPartToGraphPathPartsConverter,
            IAllParentsPathSubjectPartToGraphPathPartsConverter allParentsPathSubjectPartToGraphPathPartsConverter,
            IParentPathSubjectPartToGraphPathPartsConverter parentPathSubjectPartToGraphPathPartsConverter,
            IAllChildrenPathSubjectPartToGraphPathPartsConverter allChildrenPathSubjectPartToGraphPathPartsConverter,
            IChildrenPathSubjectPartToGraphPathPartsConverter childrenPathSubjectPartToGraphPathPartsConverter,
            IAllDowndatesPathSubjectPartToGraphPathPartsConverter allDowndatesPathSubjectPartToGraphPathPartsConverter,
            IDowndatePathSubjectPartToGraphPathPartsConverter downdatePathSubjectPartToGraphPathPartsConverter,
            IAllUpdatesPathSubjectPartToGraphPathPartsConverter allUpdatesPathSubjectPartToGraphPathPartsConverter,
            IUpdatesPathSubjectPartToGraphPathPartsConverter updatesPathSubjectPartToGraphPathPartsConverter,
            IWildcardPathSubjectPartToGraphPathPartsConverter wildcardPathSubjectPartToGraphPathPartsConverter,
            ITaggedPathSubjectPartToGraphPathPartsConverter taggedPathSubjectPartToGraphPathPartsConverter,
            ITraversingWildcardPathSubjectPartToGraphPathPartsConverter traversingWildcardPathSubjectPartToGraphPathPartsConverter,
            IConditionalPathSubjectPartToGraphPathPartsConverter conditionalPathSubjectPartToGraphPathPartsConverter)
        {
            _constantPathSubjectPartToGraphPathPartsConverter = constantPathSubjectPartToGraphPathPartsConverter;
            _identifierPathSubjectPartToGraphPathPartsConverter = identifierPathSubjectPartToGraphPathPartsConverter;
            _variablePathSubjectPartToGraphPathPartsConverter = variablePathSubjectPartToGraphPathPartsConverter;
            _allParentsPathSubjectPartToGraphPathPartsConverter = allParentsPathSubjectPartToGraphPathPartsConverter;
            _parentPathSubjectPartToGraphPathPartsConverter = parentPathSubjectPartToGraphPathPartsConverter;
            _allChildrenPathSubjectPartToGraphPathPartsConverter = allChildrenPathSubjectPartToGraphPathPartsConverter;
            _childrenPathSubjectPartToGraphPathPartsConverter = childrenPathSubjectPartToGraphPathPartsConverter;
            _allDowndatesPathSubjectPartToGraphPathPartsConverter = allDowndatesPathSubjectPartToGraphPathPartsConverter;
            _downdatePathSubjectPartToGraphPathPartsConverter = downdatePathSubjectPartToGraphPathPartsConverter;
            _allUpdatesPathSubjectPartToGraphPathPartsConverter = allUpdatesPathSubjectPartToGraphPathPartsConverter;
            _updatesPathSubjectPartToGraphPathPartsConverter = updatesPathSubjectPartToGraphPathPartsConverter;
            _wildcardPathSubjectPartToGraphPathPartsConverter = wildcardPathSubjectPartToGraphPathPartsConverter;
            _taggedPathSubjectPartToGraphPathPartsConverter = taggedPathSubjectPartToGraphPathPartsConverter;
            _traversingWildcardPathSubjectPartToGraphPathPartsConverter = traversingWildcardPathSubjectPartToGraphPathPartsConverter;
            _conditionalPathSubjectPartToGraphPathPartsConverter = conditionalPathSubjectPartToGraphPathPartsConverter;

        }

        public async Task<GraphPath> Convert(PathSubject pathSubject, ExecutionScope scope)
        {
            var builder = new GraphPathBuilder();

            //var result = new List<GraphPathPart>()

            for (var i = 0; i < pathSubject.Parts.Length; i++)
            {
                var part = pathSubject.Parts[i];
                var previousPart = i > 0 ? pathSubject.Parts[i - 1] : null;
                var nextPart = i < pathSubject.Parts.Length - 1 ? pathSubject.Parts[i + 1] : null;

                IPathSubjectPartToGraphPathPartsConverter converter = part switch
                {
                    ConstantPathSubjectPart => _constantPathSubjectPartToGraphPathPartsConverter,
                    IdentifierPathSubjectPart => _identifierPathSubjectPartToGraphPathPartsConverter,
                    VariablePathSubjectPart => _variablePathSubjectPartToGraphPathPartsConverter,
                    AllParentsPathSubjectPart => _allParentsPathSubjectPartToGraphPathPartsConverter,
                    ParentPathSubjectPart => _parentPathSubjectPartToGraphPathPartsConverter,
                    AllChildrenPathSubjectPart => _allChildrenPathSubjectPartToGraphPathPartsConverter,
                    ChildrenPathSubjectPart => _childrenPathSubjectPartToGraphPathPartsConverter,
                    AllDowndatesPathSubjectPart => _allDowndatesPathSubjectPartToGraphPathPartsConverter,
                    DowndatePathSubjectPart => _downdatePathSubjectPartToGraphPathPartsConverter,
                    AllUpdatesPathSubjectPart => _allUpdatesPathSubjectPartToGraphPathPartsConverter,
                    UpdatesPathSubjectPart => _updatesPathSubjectPartToGraphPathPartsConverter,
                    TaggedPathSubjectPart => _taggedPathSubjectPartToGraphPathPartsConverter,
                    WildcardPathSubjectPart => _wildcardPathSubjectPartToGraphPathPartsConverter,
                    TraversingWildcardPathSubjectPart => _traversingWildcardPathSubjectPartToGraphPathPartsConverter,
                    ConditionalPathSubjectPart => _conditionalPathSubjectPartToGraphPathPartsConverter,
                    _ => throw new NotSupportedException($"Cannot process path subject part: {part}")
                };

                var graphPathParts = await converter.Convert(part, i, previousPart, nextPart, scope).ConfigureAwait(false);

                builder.AddRange(graphPathParts);
            }

            return builder.ToPath();
        }
    }
}
