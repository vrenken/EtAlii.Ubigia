//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class RemoveOperatorSelector2 : Selector<OperatorParameters, IRemoveOperatorSubProcessor2>, IRemoveOperatorSelector2
//    {
//        public RemoveOperatorSelector2(
//            IRemoveByNameFromAbsolutePathProcessor2 removeByNameFromAbsolutePathProcessor,
//            IRemoveByNameFromRelativePathProcessor2 removeByNameFromRelativePathProcessor,
//            IRemoveByIdFromAbsolutePathProcessor2 removeByIdFromAbsolutePathProcessor,
//            IRemoveByIdFromRelativePathProcessor2 removeByIdFromRelativePathProcessor)
//        {
//            this.Register(p => p.RightSubject is Identifier && p.LeftSubject == null, removeByIdFromAbsolutePathProcessor)
//                .Register(p => p.RightSubject is Identifier && p.LeftSubject != null, removeByIdFromRelativePathProcessor)
//                .Register(p => p.RightSubject is DynamicNode && p.LeftSubject == null, removeByIdFromAbsolutePathProcessor)
//                .Register(p => p.RightSubject is DynamicNode && p.LeftSubject != null, removeByIdFromRelativePathProcessor)
//                .Register(p => p.RightSubject is Identifier == false && p.LeftSubject == null, removeByNameFromAbsolutePathProcessor)
//                .Register(p => p.RightSubject is Identifier == false && p.LeftSubject != null, removeByNameFromRelativePathProcessor);
//        }
//    }
//}
