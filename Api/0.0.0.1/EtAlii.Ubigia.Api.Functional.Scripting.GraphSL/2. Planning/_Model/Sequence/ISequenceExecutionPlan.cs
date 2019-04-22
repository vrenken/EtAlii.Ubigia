namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    public interface ISequenceExecutionPlan
    {
        Task<IObservable<object>> Execute(ExecutionScope scope);
    }
}