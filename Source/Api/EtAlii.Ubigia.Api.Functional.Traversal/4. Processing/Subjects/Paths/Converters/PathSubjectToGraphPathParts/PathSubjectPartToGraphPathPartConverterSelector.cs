namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Structure;

    internal class PathSubjectPartToGraphPathPartConverterSelector : Selector<PathSubjectPart, IPathSubjectPartToGraphPathPartsConverter>, IPathSubjectPartToGraphPathPartConverterSelector
    {
        public PathSubjectPartToGraphPathPartConverterSelector(
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
            Register(part => part is ConstantPathSubjectPart, constantPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is IdentifierPathSubjectPart, identifierPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is VariablePathSubjectPart, variablePathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is AllParentsPathSubjectPart, allParentsPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is ParentPathSubjectPart, parentPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is AllChildrenPathSubjectPart, allChildrenPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is ChildrenPathSubjectPart, childrenPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is AllDowndatesPathSubjectPart, allDowndatesPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is DowndatePathSubjectPart, downdatePathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is AllUpdatesPathSubjectPart, allUpdatesPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is UpdatesPathSubjectPart, updatesPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is TaggedPathSubjectPart, taggedPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is WildcardPathSubjectPart, wildcardPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is TraversingWildcardPathSubjectPart, traversingWildcardPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is ConditionalPathSubjectPart, conditionalPathSubjectPartToGraphPathPartsConverter);
        }
    }
}
