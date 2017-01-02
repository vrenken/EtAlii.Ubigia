//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Threading.Tasks;

//    internal interface IOperatorProcessor2
//    {
//        // Now.
//        void Process(
//            ProcessParameters<Operator, SequencePart> parameters, 
//            ExecutionScope scope,
//            Type leftType,
//            Type rightType,
//            IObservable<object> leftInput, 
//            IObservable<object> rightInput, 
//            IObserver<object> output);

//        // Should become:
//        //Task<object> Process(Operator @operator, ExecutionScope scope, IObserver<object> output, IObservable<object> left, IObservable<object> right);
//    }
//}
