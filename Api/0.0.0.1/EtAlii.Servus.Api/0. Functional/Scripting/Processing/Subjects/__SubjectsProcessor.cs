//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Threading.Tasks;

//    internal class SubjectsProcessor : ISubjectsProcessor
//    {
//        private readonly ISubjectProcessorSelector _selector;


//        internal SubjectsProcessor(ISubjectProcessorSelector selector)
//        {
//            _selector = selector;
//        }

//        public async Task<object> Process(
//            ProcessParameters<SequencePart, SequencePart> parameters, 
//            ExecutionScope scope,
//            IObserver<object> output)
//        {
//            var subject = parameters.Target as Subject;
//            var processor = _selector.Select(subject);

//            var subjectParameters = new ProcessParameters<Subject, SequencePart>(subject)
//            {
//                FuturePart = parameters.FuturePart,
//                LeftPart = parameters.LeftPart,
//                RightPart = parameters.RightPart,
//                RightResult = parameters.RightResult,
//                LeftResult = parameters.LeftResult,
//            };
//            return await processor.Process(subjectParameters, scope, output);
//        }
//    }
//}
