namespace EtAlii.Servus.Api.Functional
{
    using System;

    public interface ISequenceExecutionPlan
    {
        IObservable<object> Execute(ExecutionScope scope);
    }
}