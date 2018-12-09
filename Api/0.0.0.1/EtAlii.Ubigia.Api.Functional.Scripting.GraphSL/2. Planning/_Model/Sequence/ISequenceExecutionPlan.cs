namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface ISequenceExecutionPlan
    {
        IObservable<object> Execute(ExecutionScope scope);
    }
}