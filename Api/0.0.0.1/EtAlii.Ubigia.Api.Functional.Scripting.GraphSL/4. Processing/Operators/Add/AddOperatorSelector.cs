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
            this.Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is VariableSubject, addByIdToRelativePathProcessor)
                .Register(p =>  (p.LeftSubject is EmptySubject) && p.RightSubject is VariableSubject, addByIdToAbsolutePathProcessor)
                .Register(p =>  (p.LeftSubject is EmptySubject), addByNameToAbsolutePathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject), addByNameToRelativePathProcessor);
        }
    }
}
