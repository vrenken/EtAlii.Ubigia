//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using EtAlii.xTechnology.Structure;

//    internal class ConstantSubjectsProcessor2 : IConstantSubjectsProcessor2
//    {
//        private readonly ISelector<ConstantSubject, IConstantSubjectProcessor2> _selector;

//        public ConstantSubjectsProcessor2(ISelector<ConstantSubject, IConstantSubjectProcessor2> selector)
//        {
//            _selector = selector;
//        }

//        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
//        {
//            var constantSubject = (ConstantSubject)subject;
//            var processor = _selector.Select(constantSubject);

//            //var constantSubjectParameters = new ProcessParameters<Subject, SequencePart>(constantSubject)
//            //{
//            //    FuturePart = parameters.FuturePart,
//            //    LeftPart = parameters.LeftPart,
//            //    RightPart = parameters.RightPart,
//            //    RightResult = parameters.RightResult,
//            //    LeftResult = parameters.LeftResult,
//            //};
//            processor.Process(constantSubject, scope, output);
//        }
//    }
//}
