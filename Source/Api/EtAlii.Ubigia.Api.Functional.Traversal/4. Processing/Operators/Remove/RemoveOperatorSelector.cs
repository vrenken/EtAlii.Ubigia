namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Structure;

    internal class RemoveOperatorSelector : Selector<OperatorParameters, IRemoveOperatorSubProcessor>, IRemoveOperatorSelector
    {
        public RemoveOperatorSelector(
            IRemoveByNameFromAbsolutePathProcessor removeByNameFromAbsolutePathProcessor,
            IRemoveByNameFromRelativePathProcessor removeByNameFromRelativePathProcessor)
        {
            Register(p => (p.LeftSubject is EmptySubject), removeByNameFromAbsolutePathProcessor)
                .Register(p => !(p.LeftSubject is EmptySubject), removeByNameFromRelativePathProcessor);
        }
    }
}
