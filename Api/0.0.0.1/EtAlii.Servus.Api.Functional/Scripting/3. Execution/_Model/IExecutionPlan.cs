namespace EtAlii.Servus.Api.Functional
{
    using System;

    public interface IExecutionPlan
    {
        Type OutputType { get; }

        IObservable<object> Execute(ExecutionScope scope);
    }
}