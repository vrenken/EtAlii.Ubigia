namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class AddOperatorSelector : Selector<OperatorParameters, IAddOperatorSubProcessor>, IAddOperatorSelector
    {
        public AddOperatorSelector(
            IAddByNameAsNewPathProcessor addByNameAsNewPathProcessor,
            IAddByNameToExistingPathProcessor addByNameToExistingPathProcessor,
            IAddByIdToExistingPathProcessor addByIdToExistingPathProcessor,
            IAddByIdAsNewPathProcessor addByIdAsNewPathProcessor)
        {
            this.Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is VariableSubject, addByIdToExistingPathProcessor)
                .Register(p =>  (p.LeftSubject is EmptySubject) && p.RightSubject is VariableSubject, addByIdAsNewPathProcessor)
                .Register(p =>  (p.LeftSubject is EmptySubject), addByNameAsNewPathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject), addByNameToExistingPathProcessor);
        }
    }
}
