namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Structure;

    internal class AddOperatorSelector : Selector<OperatorParameters, IAddOperatorSubProcessor>, IAddOperatorSelector
    {
        public AddOperatorSelector(
            IAddByNameAsNewPathProcessor addByNameAsNewPathProcessor,
            IAddRootedPathToExistingPathProcessor addRootedPathToExistingPathProcessor,
            IAddAbsolutePathToExistingPathProcessor addAbsolutePathToExistingPathProcessor,
            IAddRelativePathToExistingPathProcessor addRelativePathToExistingPathProcessor,
            IAddVariableToExistingPathProcessor addVariableToExistingPathProcessor,
            IAddConstantToExistingPathProcessor addConstantToExistingPathProcessor,
            IAddVariableAsNewPathProcessor addVariableAsNewPathProcessor,
            IAddFunctionToExistingPathProcessor addFunctionToExistingPathProcessor)
        {
            Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is VariableSubject, addVariableToExistingPathProcessor)
                .Register(p =>  (p.LeftSubject is EmptySubject) && p.RightSubject is VariableSubject, addVariableAsNewPathProcessor)
                .Register(p =>  (p.LeftSubject is EmptySubject), addByNameAsNewPathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is RootedPathSubject, addRootedPathToExistingPathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is RelativePathSubject, addRelativePathToExistingPathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is AbsolutePathSubject, addAbsolutePathToExistingPathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is StringConstantSubject, addConstantToExistingPathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject) && p.RightSubject is FunctionSubject, addFunctionToExistingPathProcessor);
        }
    }
}
