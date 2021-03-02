namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Threading.Tasks;

    public abstract class ExecutionPlan
    {
        public abstract Type OutputType { get; }

        internal ExecutionPlanResultSink ResultSink { get; private set; }

        internal abstract Task Execute(SchemaExecutionScope executionScope);

        internal void SetResultSink(ExecutionPlanResultSink resultSink)
        {
            ResultSink = resultSink;
        }
    }
}
