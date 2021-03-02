namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System.Threading.Tasks;

    public abstract class ExecutionPlan
    {
        internal ExecutionPlanResultSink ResultSink { get; private set; }

        internal abstract Task Execute(SchemaExecutionScope executionScope);

        internal void SetResultSink(ExecutionPlanResultSink resultSink)
        {
            ResultSink = resultSink;
        }
    }
}
