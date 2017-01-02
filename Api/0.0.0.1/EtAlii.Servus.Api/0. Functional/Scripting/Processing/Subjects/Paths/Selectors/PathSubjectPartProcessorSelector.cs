namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class PathSubjectPartProcessorSelector : Selector<PathSubjectPart, IPathSubjectPartProcessor>
    {
        public PathSubjectPartProcessorSelector(
                ConstantPathSubjectPartProcessor constantPathSubjectPartProcessor,
                IdentifierPathSubjectPartProcessor identifierPathSubjectPartProcessor,
                VariablePathSubjectPartProcessor variablePathSubjectPartProcessor, 
                IsParentOfPathSubjectPartProcessor isParentOfPathSubjectPartProcessor)
        {
            this.Register(part => part is ConstantPathSubjectPart, constantPathSubjectPartProcessor)
                .Register(part => part is IdentifierPathSubjectPart, identifierPathSubjectPartProcessor)
                .Register(part => part is VariablePathSubjectPart, variablePathSubjectPartProcessor)
                .Register(part => part is IsParentOfPathSubjectPart, isParentOfPathSubjectPartProcessor);

        }
    }
}
