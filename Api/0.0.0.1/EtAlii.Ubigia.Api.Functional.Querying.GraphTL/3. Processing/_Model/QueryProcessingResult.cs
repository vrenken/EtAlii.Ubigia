namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public class QueryProcessingResult
    {
        public Query Query { get; }
        private IQueryExecutionPlan ExecutionPlan { get;  }
        public int Step { get; }
        public int Total { get; }

        public IObservable<Structure> Output { get; }
        public Structure Structure {get; }

        internal QueryProcessingResult(Query query,
            IQueryExecutionPlan executionPlan,
            int step,
            int total,
            IObservable<Structure> output, 
            Structure structure)
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
