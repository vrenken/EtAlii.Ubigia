namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class AddOperatorSelector : Selector<OperatorParameters, IAddOperatorSubProcessor>, IAddOperatorSelector
    {
        public AddOperatorSelector(
            IAddByNameToAbsolutePathProcessor addByNameToAbsolutePathProcessor,
            IAddByNameToRelativePathProcessor addByNameToRelativePathProcessor,
            IAddByIdToRelativePathProcessor addByIdToRelativePathProcessor,
            IAddByIdToAbsolutePathProcessor addByIdToAbsolutePathProcessor)
        {
            this.Register(p => p.RightSubject is VariableSubject && (p.LeftSubject is EmptySubject) == false, addByIdToRelativePathProcessor)
                .Register(p => p.RightSubject is VariableSubject && (p.LeftSubject is EmptySubject), addByIdToAbsolutePathProcessor)
                .Register(p => (p.LeftSubject is EmptySubject), addByNameToAbsolutePathProcessor)
                .Register(p => (p.LeftSubject is EmptySubject) == false, addByNameToRelativePathProcessor);
        }
    }
}
