namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;

    public class QueryProcessingResult
    {
        public Query Query { get; }
        private FragmentExecutionPlan ExecutionPlan { get;  }
        public int Step { get; }
        public int Total { get; }

        public IObservable<Structure> Output { get; }
        public ReadOnlyObservableCollection<Structure> Structure {get; }

        internal QueryProcessingResult(Query query,
            FragmentExecutionPlan executionPlan,
            int step,
            int total,
            IObservable<Structure> output, 
            ReadOnlyObservableCollection<Structure> structure)
        {
            Query = query;
            ExecutionPlan = executionPlan;
            Step = step;
            Total = total;
            Output = output;
            Structure = structure;
        }
    }
}
