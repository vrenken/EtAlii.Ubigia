namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    public interface IExecutionPlan
    {
        Type OutputType { get; }

        Task<IObservable<object>> Execute(ExecutionScope scope);
    }
}