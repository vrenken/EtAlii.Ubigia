//namespace EtAlii.Servus.Api.Functional
//{
//    using System;

//    internal class ObjectConstantSubjectProcessor2 : IObjectConstantSubjectProcessor2
//    {
//        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
//        {
//            var value = ((ObjectConstantSubject)subject).Values;
//            output.OnNext(value);
//            output.OnCompleted();
//        }
//    }
//}
