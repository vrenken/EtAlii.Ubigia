namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class OperatorProcessorSelector : Selector<Operator, IOperatorProcessor>, IOperatorProcessorSelector
    {
        public OperatorProcessorSelector(
            IAddOperatorProcessor addOperatorProcessor,
            IAssignOperatorProcessor assignOperatorProcessor,
            IRemoveOperatorProcessor removeOperatorProcessor)
        {
            this.Register(@operator => @operator is AddOperator, addOperatorProcessor)
                .Register(@operator => @operator is AssignOperator, assignOperatorProcessor)
                .Register(@operator => @operator is RemoveOperator, removeOperatorProcessor);
        }
    }
}
