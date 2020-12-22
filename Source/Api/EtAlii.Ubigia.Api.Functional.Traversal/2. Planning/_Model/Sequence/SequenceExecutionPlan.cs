namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class SequenceExecutionPlan : ISequenceExecutionPlan
    {
        private readonly IScriptExecutionPlan _startExecutionPlan;

        /// <summary>
        /// An empty SequenceExecutionPlan.
        /// </summary>
        public static ISequenceExecutionPlan Empty { get; } = new SequenceExecutionPlan(ScriptExecutionPlan.Empty);

        public SequenceExecutionPlan(IScriptExecutionPlan startExecutionPlan)
        {
            _startExecutionPlan = startExecutionPlan;
        }

        public Task<IObservable<object>> Execute(ExecutionScope scope)
        {
            return _startExecutionPlan.Execute(scope);
        }

        public override string ToString()
        {
            return _startExecutionPlan.ToString();
        }
    }
}
