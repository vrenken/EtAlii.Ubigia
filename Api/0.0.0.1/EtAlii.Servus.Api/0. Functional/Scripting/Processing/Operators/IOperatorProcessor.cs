namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal interface IOperatorProcessor
    {
        Task<object> Process(ProcessParameters<Operator, SequencePart> parameters, ExecutionScope scope, IObserver<object> output);
    }
}
