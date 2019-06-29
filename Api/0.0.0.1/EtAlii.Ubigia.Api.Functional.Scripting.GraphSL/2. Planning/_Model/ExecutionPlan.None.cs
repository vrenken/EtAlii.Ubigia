namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class ScriptExecutionPlan
    {
        public static readonly IScriptExecutionPlan Empty = new EmptyExecutionPlan();

        private class EmptyExecutionPlan : IScriptExecutionPlan
        {
            public Type OutputType { get; }

            public EmptyExecutionPlan()
            {
                OutputType = GetType();
            }

            public Task<IObservable<object>> Execute(ExecutionScope scope)
            {
                return Task.FromResult(Observable.Empty<object>());
            }

            public override string ToString()
            {
                return "[Empty]";
            }
        }
    }
}