//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class AssignOperatorSelector2 : Selector<OperatorParameters, IAssignOperatorSubProcessor2>, IAssignOperatorSelector2
//    {
//        public AssignOperatorSelector2(
//            IAssignToVariableProcessor2 assignToVariableProcessor,
//            IAssignToOutputProcessor2 assignToOutputProcessor,
//            IAssignToPathProcessor2 assignToPathProcessor,
//            IAssignToFunctionProcessor2 assignToFunctionProcessor)
//        {
//                this.Register(p => p.LeftSubject is VariableSubject, assignToVariableProcessor)
//                    .Register(p => p.LeftSubject is FunctionSubject, assignToFunctionProcessor)
//                    .Register(p => p.LeftSubject is PathSubject, assignToPathProcessor)
//                    //.Register(p => p.LeftSubject is PathSubject && IsDynamicObject(p.RightResult), assignDynamicObjectToPathProcessor)
//                    //.Register(p => p.LeftSubject is PathSubject && p.RightResult is IInternalNode, assignNodeToPathProcessor)
//                    //.Register(p => p.LeftSubject is PathSubject && p.RightResult is IPropertyDictionary, assignDictionaryToPathProcessor)
//                    .Register(p => p.LeftSubject == null, assignToOutputProcessor);
            
//        }
//    }
//}
