//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class OperatorProcessorSelector2 : Selector<Operator, IOperatorProcessor2>, IOperatorProcessorSelector2
//    {
//        public OperatorProcessorSelector2(
//            IAddOperatorProcessor2 addOperatorProcessor,
//            IAssignOperatorProcessor2 assignOperatorProcessor,
//            IRemoveOperatorProcessor2 removeOperatorProcessor)
//        {
//            this.Register(@operator => @operator is AddOperator, addOperatorProcessor)
//                .Register(@operator => @operator is AssignOperator, assignOperatorProcessor)
//                .Register(@operator => @operator is RemoveOperator, removeOperatorProcessor);
//        }
//    }
//}
