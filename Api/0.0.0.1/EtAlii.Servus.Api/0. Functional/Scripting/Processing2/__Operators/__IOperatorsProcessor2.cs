//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Threading.Tasks;

//    internal interface IOperatorsProcessor2 //: ISequencePartProcessor2
//    {
//        // Now.
//        Task<object> Process(ProcessParameters<SequencePart, SequencePart> parameters, ExecutionScope scope, IObserver<object> output);

//        // Should become:
//        //Task<object> Process(Operator @operator, ExecutionScope scope, IObserver<object> output, IObservable<object> left, IObservable<object> right);
//    }
//}