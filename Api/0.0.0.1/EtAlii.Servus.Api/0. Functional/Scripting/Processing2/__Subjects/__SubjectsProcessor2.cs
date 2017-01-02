//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Threading.Tasks;

//    internal class SubjectsProcessor2 : ISubjectsProcessor2
//    {
//        private readonly ISubjectProcessorSelector2 _selector;


//        internal SubjectsProcessor2(ISubjectProcessorSelector2 selector)
//        {
//            _selector = selector;
//        }

//        public Task<object> Process(
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
//            processor.Process(subjectParameters, scope, output);

//            return Task.FromResult<object>(null);
//        }
//    }
//}
