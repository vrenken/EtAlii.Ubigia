namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class SequenceExecutionPlan : ISequenceExecutionPlan
    {
        private readonly IExecutionPlan _startExecutionPlan;

        public static readonly ISequenceExecutionPlan Empty = new SequenceExecutionPlan(ExecutionPlan.Empty);

        public SequenceExecutionPlan(IExecutionPlan startExecutionPlan)
        {
            _startExecutionPlan = startExecutionPlan;
        }

        public IObservable<object> Execute(ExecutionScope scope)
        {
            return _startExecutionPlan.Execute(scope);
        }

        public override string ToString()
        {
            return _startExecutionPlan.ToString();
        }
    }
}