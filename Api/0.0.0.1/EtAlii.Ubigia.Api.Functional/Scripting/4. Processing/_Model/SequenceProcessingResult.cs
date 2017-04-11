namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class SequenceProcessingResult
    {
        public Sequence Sequence { get; }
        private ISequenceExecutionPlan ExecutionPlan { get;  }
        public int Step { get; }
        public int Total { get; }

        public IObservable<object> Output { get; }

        public SequenceProcessingResult(
            Sequence sequence,
            ISequenceExecutionPlan executionPlan,
            int step, 
            int total, 
            IObservable<object> output)
        {
            Sequence = sequence;
            ExecutionPlan = executionPlan;
            Step = step;
            Total = total;
            Output = output;
        }
    }
}
