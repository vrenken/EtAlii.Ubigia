namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class SequenceProcessingResult
    {
        public Sequence Sequence { get; private set; }
        public ISequenceExecutionPlan ExecutionPlan { get; private set; }
        public int Step { get; private set; }
        public int Total { get; private set; }

        public IObservable<object> Output { get; private set; }

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
