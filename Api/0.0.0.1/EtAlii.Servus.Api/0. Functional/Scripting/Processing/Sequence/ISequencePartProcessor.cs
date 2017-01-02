namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal interface ISequencePartProcessor
    {
        Task<object> Process(
            ProcessParameters<SequencePart, 
            SequencePart> parameters, 
            ExecutionScope scope,
            IObserver<object> output);
    }
}
