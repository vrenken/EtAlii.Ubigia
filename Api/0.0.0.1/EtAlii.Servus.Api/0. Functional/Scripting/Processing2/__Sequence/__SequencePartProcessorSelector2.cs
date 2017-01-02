//namespace EtAlii.Servus.Api.Functional
//{
//    using EtAlii.xTechnology.Structure;

//    internal class SequencePartProcessorSelector2 : Selector<SequencePart, ISequencePartProcessor2>, ISequencePartProcessorSelector2
//    {
//        internal SequencePartProcessorSelector2(
//            IOperatorsProcessor2 operatorsProcessor,
//            ISubjectsProcessor2 subjectsProcessor,
//            ICommentProcessor2 commentProcessor)
//        {
//            this.Register(part => part is Operator, operatorsProcessor)
//                .Register(part => part is Subject, subjectsProcessor)
//                .Register(part => part is Comment, commentProcessor);
//        }
//    }
//}
