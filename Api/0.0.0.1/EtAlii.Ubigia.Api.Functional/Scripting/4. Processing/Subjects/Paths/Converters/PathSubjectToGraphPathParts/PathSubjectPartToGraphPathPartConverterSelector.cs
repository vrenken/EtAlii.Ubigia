namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class PathSubjectPartToGraphPathPartConverterSelector : Selector<PathSubjectPart, IPathSubjectPartToGraphPathPartsConverter>, IPathSubjectPartToGraphPathPartConverterSelector
    {
        public PathSubjectPartToGraphPathPartConverterSelector(
            IConstantPathSubjectPartToGraphPathPartsConverter constantPathSubjectPartToGraphPathPartsConverter,
            IIdentifierPathSubjectPartToGraphPathPartsConverter identifierPathSubjectPartToGraphPathPartsConverter,
            IVariablePathSubjectPartToGraphPathPartsConverter variablePathSubjectPartToGraphPathPartsConverter,
            IIsParentOfPathSubjectPartToGraphPathPartsConverter isParentOfPathSubjectPartToGraphPathPartsConverter,
            IIsChildOfPathSubjectPartToGraphPathPartsConverter isChildOfPathSubjectPartToGraphPathPartsConverter,
            IDowndatePathSubjectPartToGraphPathPartsConverter downdatePathSubjectPartToGraphPathPartsConverter,
            IUpdatePathSubjectPartToGraphPathPartsConverter updatePathSubjectPartToGraphPathPartsConverter,
            IWildcardPathSubjectPartToGraphPathPartsConverter wildcardPathSubjectPartToGraphPathPartsConverter,
            ITraversingWildcardPathSubjectPartToGraphPathPartsConverter traversingWildcardPathSubjectPartToGraphPathPartsConverter,
            IConditionalPathSubjectPartToGraphPathPartsConverter conditionalPathSubjectPartToGraphPathPartsConverter)
        {
            Register(part => part is ConstantPathSubjectPart, constantPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is IdentifierPathSubjectPart, identifierPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is VariablePathSubjectPart, variablePathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is IsParentOfPathSubjectPart, isParentOfPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is IsChildOfPathSubjectPart, isChildOfPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is DowndateOfPathSubjectPart, downdatePathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is UpdatesOfPathSubjectPart, updatePathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is WildcardPathSubjectPart, wildcardPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is TraversingWildcardPathSubjectPart, traversingWildcardPathSubjectPartToGraphPathPartsConverter)
                .Register(part => part is ConditionalPathSubjectPart, conditionalPathSubjectPartToGraphPathPartsConverter);
        }
    }
}
