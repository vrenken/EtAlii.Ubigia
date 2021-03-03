namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal partial class SchemaProcessor
    {
        public async Task<TResult> ProcessSingle<TResult>(Schema schema, IResultMapper<TResult> resultMapper)
        {
            return await ProcessMultiple(schema, resultMapper)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async IAsyncEnumerable<TResult> ProcessMultiple<TResult>(Schema schema, IResultMapper<TResult> resultMapper)
        {
            // We need to create execution plans for all of the sequences.
            var executionPlans = _schemaExecutionPlanner.Plan(schema);
            var rootMetadata = executionPlans?.FirstOrDefault()?.ResultSink ?? new ExecutionPlanResultSink(null, Array.Empty<ExecutionPlanResultSink>());

            try
            {
                var executionScope = new SchemaExecutionScope();

                foreach (var executionPlan in executionPlans!)
                {
                    await executionPlan
                        .Execute(executionScope)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                while (e is AggregateException aggregateException)
                {
                    if (aggregateException.InnerException != null)
                    {
                        e = aggregateException.InnerException;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            foreach (var item in rootMetadata.Items)
            {
                yield return resultMapper.MapRoot(item);
            }
        }
    }
}
