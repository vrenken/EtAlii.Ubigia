namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    public interface ISequenceExecutionPlan
    {
        Task<IObservable<object>> Execute(ExecutionScope scope);
    }
}
