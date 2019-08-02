namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

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
        
        /// <summary>
        /// Awaiting this method ensures GSL sequence processing has finished.
        /// </summary>
        /// <returns></returns>
        public async Task Completed()
        {
            await Output.LastOrDefaultAsync();
        }

    }
}
