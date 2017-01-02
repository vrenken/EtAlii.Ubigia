//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class AssignOperatorSelector : Selector<OperatorParameters, IAssignOperatorSubProcessor>, IAssignOperatorSelector
//    {
//        public AssignOperatorSelector(
//            IAssignToVariableProcessor assignToVariableProcessor,
//            IAssignToOutputProcessor assignToOutputProcessor,
//            IAssignToPathProcessor assignToPathProcessor,
//            IAssignToFunctionProcessor assignToFunctionProcessor)
//        {
//            this.Register(p => p.LeftSubject is VariableSubject, assignToVariableProcessor)
//                .Register(p => p.LeftSubject is FunctionSubject, assignToFunctionProcessor)
//                .Register(p => p.LeftSubject is PathSubject, assignToPathProcessor)
//                .Register(p => p.LeftSubject is EmptySubject, assignToOutputProcessor);

//        }
//    }
//}
