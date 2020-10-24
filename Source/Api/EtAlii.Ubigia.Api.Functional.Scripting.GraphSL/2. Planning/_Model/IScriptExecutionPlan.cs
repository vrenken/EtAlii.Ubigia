namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using System;
    using System.Threading.Tasks;

    public interface IScriptExecutionPlan
    {
        Type OutputType { get; }

        Task<IObservable<object>> Execute(ExecutionScope scope);
    }
}