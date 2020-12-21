namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    internal class OperatorParsingScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            // Operators
            container.Register<IOperatorsParser, OperatorsParser>();
            container.Register<IAssignOperatorParser, AssignOperatorParser>();
            container.Register<IAddOperatorParser, AddOperatorParser>();
            container.Register<IRemoveOperatorParser, RemoveOperatorParser>();
        }
    }
}
