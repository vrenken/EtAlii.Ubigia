// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class ScriptExecutionPlan
    {
        /// <summary>
        /// An empty ScriptExecutionPlan.
        /// </summary>
        public static IScriptExecutionPlan Empty { get; } = new EmptyExecutionPlan();

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
