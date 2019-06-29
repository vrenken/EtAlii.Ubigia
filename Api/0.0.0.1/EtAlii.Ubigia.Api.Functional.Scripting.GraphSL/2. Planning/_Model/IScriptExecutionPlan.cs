namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    public interface IScriptExecutionPlan
    {
        Type OutputType { get; }

        Task<IObservable<object>> Execute(ExecutionScope scope);
    }
}