namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api.Data.Model;
    using EtAlii.xTechnology.Structure;

    internal class OperatorProcessorSelector : Selector<Operator, IOperatorProcessor>
    {
        public OperatorProcessorSelector(
            AddOperatorProcessor addOperatorProcessor,
            AssignOperatorProcessor assignOperatorProcessor,
            RemoveOperatorProcessor removeOperatorProcessor)
        {
            this.Register(@operator => @operator is AddOperator, addOperatorProcessor)
                .Register(@operator => @operator is AssignOperator, assignOperatorProcessor)
                .Register(@operator => @operator is RemoveOperator, removeOperatorProcessor);
        }
    }
}
