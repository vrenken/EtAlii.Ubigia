//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class ConstantSubjectProcessorSelector2 : Selector<ConstantSubject, IConstantSubjectProcessor2>, IConstantSubjectProcessorSelector2
//    {
//        public ConstantSubjectProcessorSelector2(
//            IStringConstantSubjectProcessor2 stringConstantSubjectProcessor,
//            IObjectConstantSubjectProcessor2 objectConstantSubjectProcessor)
//        {
//            this.Register(subject => subject is StringConstantSubject, stringConstantSubjectProcessor)
//                .Register(subject => subject is ObjectConstantSubject, objectConstantSubjectProcessor);
//        }
//    }
//}
