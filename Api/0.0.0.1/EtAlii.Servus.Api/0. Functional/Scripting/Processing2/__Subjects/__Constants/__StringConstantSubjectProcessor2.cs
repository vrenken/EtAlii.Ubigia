//namespace EtAlii.Servus.Api.Functional
//{
//    using System;

//    internal class StringConstantSubjectProcessor2 : IStringConstantSubjectProcessor2
//    {
//        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
//        {
//            var value = ((StringConstantSubject)subject).Value;
//            output.OnNext(value);
//            output.OnCompleted();
//        }
//    }
//}
