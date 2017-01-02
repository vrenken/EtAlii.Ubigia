//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal partial class AddOperatorSelector2 : Selector<OperatorParameters, IAddOperatorSubProcessor2>, IAddOperatorSelector2
//    {
//        public AddOperatorSelector2(
//            IAddByNameToAbsolutePathProcessor2 addByNameToAbsolutePathProcessor,
//            IAddByNameToRelativePathProcessor2 addByNameToRelativePathProcessor,
//            IAddByIdToAbsolutePathProcessor2 addByIdToAbsolutePathProcessor,
//            IAddByIdToRelativePathProcessor2 addByIdToRelativePathProcessor)
//        {
//                this//.Register(p => p.RightSubject is VariableSubject && p.LeftInput != null, //addByIdToAbsolutePathProcessor)
//                    //.Register(p => p.RightSubject is VariableSubject && p.LeftInput == null, //addByIdToAbsolutePathProcessor)
//                    //.Register(p => p.RightSubject is Identifier && p.LeftInput != null, addByIdToRelativePathProcessor)
//                    //.Register(p => p.RightSubject is Identifier && p.LeftInput != null, addByIdToRelativePathProcessor)
//                    //.Register(p => p.RightSubject is DynamicNode && p.LeftInput == null, addByIdToAbsolutePathProcessor)
//                    //.Register(p => p.RightSubject is DynamicNode && p.LeftInput != null, addByIdToRelativePathProcessor)
//                    .Register(p => p.LeftSubject == null, addByNameToAbsolutePathProcessor)
//                    .Register(p => p.LeftSubject != null, addByNameToRelativePathProcessor);
//        }
//    }
//}
